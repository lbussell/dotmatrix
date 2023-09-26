namespace DotMatrix.Core;

using System.Reflection;
using DotMatrix.Core.Opcodes;

public class Disassembler
{
    private readonly Dictionary<byte, IOpcode> _opcodes = RegisterOpcodes(true);

    public IEnumerable<Instruction> Disassemble(IReadableMemory rom)
    {
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

        for (int y = 0; y <= 0xF0; y += 0x10)
        {
            Console.WriteLine("\n------------------------------------");
            for (int x = 0; x <= 0xF; x += 1)
            {
                // string op = (x | y).ToString("X2");

                string op = instructions.TryGetValue((byte)(x | y), out IOpcode _)
                    ? "✅ "
                    : "❌ ";

                Console.Write($"| {op} ");
            }

            Console.Write("|");
        }

        Console.WriteLine("\n------------------------------------");

        return instructions;
    }
}
