namespace DotMatrix.Core.Instructions;

public partial class OpcodeHandler
{
    private const byte B = 0;
    private const byte C = 1;
    private const byte D = 2;
    private const byte E = 3;
    private const byte H = 4;
    private const byte L = 5;
    private const byte HLIndirect = 6;
    private const byte A = 7;

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

    private static void Load8(ref CpuState state, IBus bus)
    {
        state.TCycles += MCycleLength;
        int block = (state.Ir & 0b_1100_0000) >> 6;
        switch (block)
        {
            case 0:
                Load8Block0(ref state, bus);
                break;
            case 1:
                Load8Block1(ref state, bus);
                break;
            case 3:
                Load8Block3(ref state, bus);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private static void Load8Block0(ref CpuState state, IBus bus)
    {
        switch (state.Ir & 0b_1111)
        {
            // X110 = 0110 or 1110
            case 0b_0110:
            case 0b_1110:
            {
                // LD R8 <- i8
                byte target = (byte)((state.Ir & 0b_0011_1000) >> 3);
                byte value = Immediate8(ref state, bus);
                SetR8(ref state, bus, value, target);
                break;
            }
            case 0b_0010:
            {
                // LD [R16] <- A
                byte target = (byte)((state.Ir & 0b_0011_0000) >> 4);
                SetR16Mem(ref state, bus, target, state.A);
                break;
            }
            case 0b_1010:
            {
                // LD A <- [R16]
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

    // LD R <- R`
    private static void Load8Block1(ref CpuState state, IBus bus)
    {
        byte source = (byte)(state.Ir & 0b_0000_0111);
        byte target = (byte)((state.Ir & 0b_0011_1000) >> 3);

        SetR8(ref state, bus,
            value: GetR8(ref state, bus, source),
            target: target);
    }

    private static void Load8Block3(ref CpuState state, IBus bus)
    {
        state.TCycles += MCycleLength;
        switch (state.Ir & 0b_0001_1111)
        {
            case (0b_00010): // LDH [0xFF00+C] <- A
                bus[(ushort)(0xFF00 + state.C)] = state.A;
                break;
            case (0b_00000): // LDH [0xFF00+imm8] <- A
                bus[(ushort)(0xFF00 + Immediate8(ref state, bus))] = state.A;
                break;
            case (0b_01010): // LDH [imm16] <- A
                bus[Immediate16(ref state, bus)] = state.A;
                break;
            case (0b_10010): // LDH A <- [0xFF00+C]
                state.A = bus[(ushort)(0xFF00 + state.C)];
                break;
            case (0b_10000): // LDH A <- [0xFF00+imm8]
                state.A = bus[(ushort)(0xFF00 + Immediate8(ref state, bus))];
                break;
            case (0b_11010): // LDH A <- [imm16]
                state.A = bus[Immediate16(ref state, bus)];
                break;
            default:
                Panic(nameof(Load8Block3), state.Ir);
                break;
        }
    }
}
