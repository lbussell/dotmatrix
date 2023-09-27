using System.Text;

namespace DotMatrix.Core;

using System.Reflection;
using DotMatrix.Core.Opcodes;

public class Disassembler
{
    private readonly Dictionary<byte, IOpcode> _opcodes = RegisterOpcodes(true);

    public IEnumerable<Instruction> Disassemble(IReadableMemory rom)
    {
        PrintOpcodeTable(_opcodes);

        for (ushort addr = 0; addr < rom.Length;)
        {
            byte opcodeByte = rom.Read8(addr);

            if (!_opcodes.TryGetValue(opcodeByte, out IOpcode opcode))
            {
                throw new OpcodeNotFoundException(opcodeByte);
            }

            addr += 1;
            Instruction instruction = opcode.ReadType switch
            {
                ReadType.None => new Instruction(opcode, null),
                ReadType.Read8 => new Instruction(opcode, ReadInc8(rom, ref addr)),
                ReadType.Read16 => new Instruction(opcode, ReadInc16(rom, ref addr)),
                _ => throw new NotSupportedException($"Opcode {opcode.Name} ReadType {opcode.ReadType} not supported."),
            };
            yield return instruction;
        }
    }

    private static void PrintOpcodeTable(Dictionary<byte, IOpcode> opcodes)
    {
        int cellWidth = 8;
        char vLine = '\u2502';
        char hLine = '\u2500';
        char cross = '\u253c';

        for (int y = -0x10; y <= 0xF8; y += 0x08)
        {
            StringBuilder line = new();

            if (y == -0x10)
            {
                line.Append(string.Empty.PadLeft(cellWidth));
            }

            bool isHorizontalLine = (y & 0x0F) == 0x08;

            for (int x = -0x1; x <= 0xF; x += 0x1)
            {
                if (isHorizontalLine)
                {
                    line.Append(new string(hLine, cellWidth) + (x == 0xF ? string.Empty : cross));
                }
                else if (y == -0x10 && x == -0x01)
                {
                }
                else if (y == -0x10)
                {
                    line.Append($"{vLine}   +{x:X1}   ");
                }
                else if (x == -0x1)
                {
                    line.Append($"{y:X2}+ ".PadLeft(cellWidth));
                }
                else
                {
                    string op = opcodes.TryGetValue((byte)(x | y), out IOpcode _)
                        ? "   YY   "
                        : "        ";
                    line.Append($"|{op}");
                }
            }

            line.Append('|');
            Console.WriteLine(line.ToString());
        }
    }

    private static byte ReadInc8(IReadableMemory data, ref ushort addr) => data.Read8(addr++);

    private static ushort ReadInc16(IReadableMemory data, ref ushort addr)
    {
        ushort val = data.Read16(addr);
        addr += 2;
        return val;
    }

    private static Dictionary<byte, IOpcode> RegisterOpcodes(bool debugOutput = false)
    {
        Dictionary<byte, IOpcode> instructions = new();

        foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
        {
            IEnumerable<Attribute> attributes = type.GetCustomAttributes(typeof(OpcodeAttribute));
            foreach (Attribute attribute in attributes)
            {
                OpcodeAttribute opcodeAttribute = (OpcodeAttribute)attribute;
                IOpcode instruction = opcodeAttribute.R2 != CpuRegister.Implied
                    ? (IOpcode)Activator.CreateInstance(type, opcodeAttribute.R, opcodeAttribute.R2)!
                    : opcodeAttribute.R != CpuRegister.Implied
                        ? (IOpcode)Activator.CreateInstance(type, opcodeAttribute.R)!
                        : (IOpcode)Activator.CreateInstance(type)!;

                instructions[opcodeAttribute.Opcode] = instruction;

                if (debugOutput)
                {
                    Console.WriteLine($"Registered opcode 0x{opcodeAttribute.Opcode:X2}: {instruction.Name}");
                }
            }
        }

        return instructions;
    }
}
