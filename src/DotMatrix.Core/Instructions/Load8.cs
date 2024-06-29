namespace DotMatrix.Core.Instructions;

public static class Load8
{
    public static void Load8Impl(ref CpuState state, IBus bus)
    {
        state.IncrementMCycles();
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
                byte value = Common.Immediate8(ref state, bus);
                Common.SetR8(ref state, bus, value, target);
                break;
            }
            case 0b_0010:
            {
                // LD [R16] <- A
                byte target = (byte)((state.Ir & 0b_0011_0000) >> 4);
                Common.SetR16Mem(ref state, bus, target, state.A);
                break;
            }
            case 0b_1010:
            {
                // LD A <- [R16]
                byte source = (byte)((state.Ir & 0b_0011_0000) >> 4);
                byte value = Common.GetR16Mem(ref state, bus, source);
                Common.SetR8(ref state, bus, value, Common.A);
                break;
            }
            default:
                Common.Panic(nameof(Load8Block0), state.Ir);
                break;
        }
    }

    // LD R <- R`
    private static void Load8Block1(ref CpuState state, IBus bus)
    {
        byte source = (byte)(state.Ir & 0b_0000_0111);
        byte target = (byte)((state.Ir & 0b_0011_1000) >> 3);

        Common.SetR8(ref state, bus,
            value: Common.GetR8(ref state, bus, source),
            target: target);
    }

    private static void Load8Block3(ref CpuState state, IBus bus)
    {
        state.IncrementMCycles();
        switch (state.Ir & 0b_0001_1111)
        {
            case (0b_00010): // LDH [0xFF00+C] <- A
                bus[(ushort)(0xFF00 + state.C)] = state.A;
                break;
            case (0b_00000): // LDH [0xFF00+imm8] <- A
                bus[(ushort)(0xFF00 + Common.Immediate8(ref state, bus))] = state.A;
                break;
            case (0b_01010): // LDH [imm16] <- A
                bus[Common.Immediate16(ref state, bus)] = state.A;
                break;
            case (0b_10010): // LDH A <- [0xFF00+C]
                state.A = bus[(ushort)(0xFF00 + state.C)];
                break;
            case (0b_10000): // LDH A <- [0xFF00+imm8]
                state.A = bus[(ushort)(0xFF00 + Common.Immediate8(ref state, bus))];
                break;
            case (0b_11010): // LDH A <- [imm16]
                state.A = bus[Common.Immediate16(ref state, bus)];
                break;
            default:
                Common.Panic(nameof(Load8Block3), state.Ir);
                break;
        }
    }
}
