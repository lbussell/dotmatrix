using System.Reflection.Metadata.Ecma335;

namespace DotMatrix.Core;

public class OpcodeHandler
{
    private const int MCycleLength = 4;

    private delegate void Instruction(ref CpuState state, IBus bus);

    private static readonly Instruction[] s_instructions =
    [
        NoOp,       Load16,     Load8,      Inc16,      Inc8,       Dec8,       Load8,      Rlca,
        Load16,     Add16,      Load8,      Dec16,      Inc8,       Dec8,       Load8,      Rrca,
        Stop,       Load16,     Load8,      Inc16,      Inc8,       Dec8,       Load8,      Rla,
        Jr,         Add16,      Load8,      Dec16,      Inc8,       Dec8,       Load8,      Rra,
        Jr,         Load16,     Load8,      Inc16,      Inc8,       Dec8,       Load8,      Daa,
        Jr,         Add16,      Load8,      Dec16,      Inc8,       Dec8,       Load8,      Cpl,
        Jr,         Load16,     Load8,      Inc16,      Inc8,       Dec8,       Load8,      Scf,
        Jr,         Add16,      Load8,      Dec16,      Inc8,       Dec8,       Load8,      Ccf,

        Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,
        Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,
        Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,
        Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,
        Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,
        Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,
        Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Halt,       Load8,
        Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,

        Add,        Add,        Add,        Add,        Add,        Add,        Add,        Add,
        Adc,        Adc,        Adc,        Adc,        Adc,        Adc,        Adc,        Adc,
        Sub,        Sub,        Sub,        Sub,        Sub,        Sub,        Sub,        Sub,
        Sbc,        Sbc,        Sbc,        Sbc,        Sbc,        Sbc,        Sbc,        Sbc,
        And,        And,        And,        And,        And,        And,        And,        And,
        Xor,        Xor,        Xor,        Xor,        Xor,        Xor,        Xor,        Xor,
        Or,         Or,         Or,         Or,         Or,         Or,         Or,         Or,
        Cp,         Cp,         Cp,         Cp,         Cp,         Cp,         Cp,         Cp,

        Ret,        Pop,        Jp,         Jp,         Call,       Push,       Add,        Rst,
        Ret,        Ret,        Jp,         Cb,         Call,       Call,       Adc,        Rst,
        Ret,        Pop,        Jp,         Undef,      Call,       Push,       Sub,        Rst,
        Ret,        Reti,       Jp,         Undef,      Call,       Undef,      Sbc,        Rst,
        Load8,      Pop,        Load8,      Undef,      Undef,      Push,       And,        Rst,
        Add16,      Jp,         Load8,      Undef,      Undef,      Undef,      Xor,        Rst,
        Load8,      Pop,        Load8,      Di,         Undef,      Push,       Or,         Rst,
        Add16,      Reti,       Load8,      Di,         Undef,      Undef,      Cp,         Rst,
    ];

    public void HandleOpcode(ref CpuState state, IBus bus)
    {
        s_instructions[state.Ir](ref state, bus);
    }

    private static void Cb(ref CpuState state, IBus bus)
    {
        state.IrIsCb = true;
        throw new NotImplementedException();
    }

    private static void Di(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Reti(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Push(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Call(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Jp(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Pop(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Ret(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Cp(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Or(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Xor(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Ccf(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Scf(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Cpl(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Daa(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Halt(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Sbc(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void And(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Rra(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Rla(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Adc(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Sub(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Rlca(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Undef(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Rst(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Rrca(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Dec8(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Dec16(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Add16(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Stop(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Jr(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Add(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Inc8(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Inc16(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Load8(ref CpuState state, IBus bus)
    {
        state.TCycles += MCycleLength;
        int block = (state.Ir & 0b_1100_0000) >> 6;
        switch (block)
        {
            case 0b_00:
                Load8Block0(ref state, bus);
                break;
            case 0b_01:
                Load8Block1(ref state, bus);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private static void Load8Block0(ref CpuState state, IBus bus)
    {
        switch (state.Ir & 0b_1111)
        {
            case 0b_0110:
            case 0b_1110:
            {
                byte target = (byte)((state.Ir & 0b_0011_1000) >> 3);
                byte value = Immediate8(ref state, bus);
                SetR8(ref state, bus, value, target);
                break;
            }
            case 0b_0010:
            {
                byte target = (byte)((state.Ir & 0b_0011_0000) >> 4);
                SetR16Mem(ref state, bus, target, state.A);
                break;
            }
            case 0b_1010:
            {
                byte source = (byte)((state.Ir & 0b_0011_0000) >> 4);
                byte value = GetR16Mem(ref state, bus, source);
                SetR8(ref state, bus, value, A);
                break;
            }
            default:
                Panic(nameof(Load8Block0), state.Ir);
                break;
        }
    }

    private static void Load8Block1(ref CpuState state, IBus bus)
    {
        byte source = (byte)(state.Ir & 0b_0000_0111);
        byte target = (byte)((state.Ir & 0b_0011_1000) >> 3);

        SetR8(ref state, bus,
            value: GetR8(ref state, bus, source),
            target: target);
    }

    private static void Load16(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void NoOp(ref CpuState state, IBus bus)
    {
        state.TCycles += 4;
    }

    private static byte Immediate8(ref CpuState state, IBus bus)
    {
        state.TCycles += MCycleLength;
        return bus[state.Pc++];
    }

    private static byte GetR8(ref CpuState state, IBus bus, byte target)
    {
        return target switch
        {
            0 => state.B,
            1 => state.C,
            2 => state.D,
            3 => state.E,
            4 => state.H,
            5 => state.L,
            6 => IndirectGet(ref state, bus, state.HL),
            _ => state.A,
        };
    }

    private const byte B = 0;
    private const byte C = 1;
    private const byte D = 2;
    private const byte E = 3;
    private const byte H = 4;
    private const byte L = 5;
    private const byte HLIndirect = 6;
    private const byte A = 7;

    private static void SetR8(ref CpuState state, IBus bus, byte value, byte target)
    {
        switch (target)
        {
            case B:
                state.B = value;
                break;
            case C:
                state.C = value;
                break;
            case D:
                state.D = value;
                break;
            case E:
                state.E = value;
                break;
            case H:
                state.H = value;
                break;
            case L:
                state.L = value;
                break;
            case HLIndirect: // Special case for indirect HL
                IndirectSet(ref state, bus, state.HL, value);
                break;
            default: // 7
                state.A = value;
                break;
        }
    }

    private static byte GetR16Mem(ref CpuState state, IBus bus, byte target)
    {
        state.TCycles += MCycleLength;
        return target switch
        {
            0 => bus[state.BC],
            1 => bus[state.DE],
            2 => bus[state.HL++],
            3 => bus[state.HL--],
            _ => throw new ArgumentException($"{nameof(target)} should be in range [0,3]")
        };
    }

    private static void SetR16Mem(ref CpuState state, IBus bus, byte target, byte value)
    {
        state.TCycles += MCycleLength;
        switch (target)
        {
            case 0:
                bus[state.BC] = value;
                break;
            case 1:
                bus[state.DE] = value;
                break;
            case 2:
                bus[state.HL++] = value;
                break;
            case 3:
                bus[state.HL--] = value;
                break;
            default:
                throw new ArgumentException($"{nameof(target)} should be in range [0,3]");
        }
    }

    private static byte IndirectGet(ref CpuState state, IBus bus, ushort address)
    {
        state.TCycles += MCycleLength;
        return bus[address];
    }

    private static void IndirectSet(ref CpuState state, IBus bus, ushort address, byte value)
    {
        state.TCycles += MCycleLength;
        bus[address] = value;
    }

    private static void Panic(string name, byte opcode) =>
        throw new ArgumentException($"Unexpected opcode ${opcode:X2} assigned to {name} instruction");
}
