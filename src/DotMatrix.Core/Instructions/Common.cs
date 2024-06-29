namespace DotMatrix.Core.Instructions;

internal static class Common
{
    public const byte B = 0;
    public const byte C = 1;
    public const byte D = 2;
    public const byte E = 3;
    public const byte H = 4;
    public const byte L = 5;
    public const byte HLIndirect = 6;
    public const byte A = 7;

    public static byte GetR8(ref CpuState state, IBus bus, byte target)
    {
        return target switch
        {
            0 => state.B,
            1 => state.C,
            2 => state.D,
            3 => state.E,
            4 => state.H,
            5 => state.L,
            6 => Common.IndirectGet(ref state, bus, state.HL),
            _ => state.A,
        };
    }

    public static void SetR8(ref CpuState state, IBus bus, byte value, byte target)
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
                Common.IndirectSet(ref state, bus, state.HL, value);
                break;
            default: // 7
                state.A = value;
                break;
        }
    }

    public static byte GetR16Mem(ref CpuState state, IBus bus, byte target)
    {
        state.IncrementMCycles();
        return target switch
        {
            0 => bus[state.BC],
            1 => bus[state.DE],
            2 => bus[state.HL++],
            3 => bus[state.HL--],
            _ => throw new ArgumentException($"{nameof(target)} should be in range [0,3]")
        };
    }

    public static void SetR16Mem(ref CpuState state, IBus bus, byte target, byte value)
    {
        state.IncrementMCycles();
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

    public static byte Immediate8(ref CpuState state, IBus bus)
    {
        state.IncrementMCycles();
        return bus[state.Pc++];
    }

    public static ushort Immediate16(ref CpuState state, IBus bus)
    {
        state.IncrementMCycles(2);
        byte lsb = bus[state.Pc++];
        byte msb = bus[state.Pc++];
        return (ushort)((msb << 8) | lsb);
    }

    public static byte IndirectGet(ref CpuState state, IBus bus, ushort address)
    {
        state.IncrementMCycles();
        return bus[address];
    }

    public static void IndirectSet(ref CpuState state, IBus bus, ushort address, byte value)
    {
        state.IncrementMCycles();
        bus[address] = value;
    }

    public static void Panic(string name, byte opcode) =>
        throw new ArgumentException($"Unexpected opcode ${opcode:X2} assigned to {name} instruction");
}
