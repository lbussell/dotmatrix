namespace DotMatrix.Core.Instructions;

public class OpcodeHandler : IOpcodeHandler
{
    public void HandleOpcode(ref CpuState state, IBus bus)
    {
        switch (state.Ir)
        {
            case 0x00:
                return; // NoOp

            case 0x01 or 0x11 or 0x21 or 0x31:
                Load16(ref state, bus);
                break;
            case 0x08:
                Load16ToMemory(ref state, bus);
                break;

            case 0x02 or 0x06 or 0x0A or 0x0E:
            case 0x12 or 0x16 or 0x1A or 0x1E:
            case 0x22 or 0x26 or 0x2A or 0x2E:
            case 0x32 or 0x36 or 0x3A or 0x3E:
                Load8Block0(ref state, bus);
                break;

            case 0x07:
                throw new NotImplementedException("Rlca");
            case 0x0F:
                throw new NotImplementedException("Rrca");
            case 0x10:
                throw new NotImplementedException("Stop");
            case 0x17:
                throw new NotImplementedException("Rla");
            case 0x18:
                throw new NotImplementedException("Jr i8");
            case 0x1F:
                throw new NotImplementedException("Rra");

            case 0x20 or 0x28 or 0x30 or 0x38:
                throw new NotImplementedException("Jr");

            case 0x27:
                throw new NotImplementedException("Daa");
            case 0x2F:
                throw new NotImplementedException("Cpl");
            case 0x37:
                throw new NotImplementedException("Scf");
            case 0x3F:
                throw new NotImplementedException("Ccf");

            case 0x76:
                throw new NotImplementedException("Halt");
            case >= 0x40 and <= 0x7F:
                Load8Block1(ref state, bus);
                break;

            /*
             * Block 2
             */
            case >= 0x80 and <= 0x87:
                Add(ref state, bus);
                break;
            case >= 0x88 and <= 0x8F:
                Adc(ref state, bus);
                break;
            case >= 0x90 and <= 0x97:
                Sub(ref state, bus);
                break;
            case >= 0x98 and <= 0x9F:
                Sbc(ref state, bus);
                break;
            case >= 0xA0 and <= 0xA7:
                And(ref state, bus);
                break;
            case >= 0xA8 and <= 0xAF:
                Xor(ref state, bus);
                break;
            case >= 0xB0 and <= 0xB7:
                Or(ref state, bus);
                break;
            case >= 0xB8 and <= 0xBF:
                Cp(ref state, bus);
                break;

            /*
             * Block 3
             */

            // 16-bit LSM
            case 0xC1 or 0xD1 or 0xE1 or 0xF1:
                Pop(ref state, bus);
                break;
            case 0xC5 or 0xD5 or 0xE5 or 0xF5:
                Push(ref state, bus);
                break;
            case 0xF9:
                Load16SpHl(ref state);
                break;

            // 8-bit ALU
            case 0xC6:
                AddImmediate(ref state, bus);
                break;
            case 0xCE:
                AdcImmediate(ref state, bus);
                break;
            case 0xD6:
                SubImmediate(ref state, bus);
                break;
            case 0xDE:
                SbcImmediate(ref state, bus);
                break;
            case 0xE6:
                AndImmediate(ref state, bus);
                break;
            case 0xEE:
                XorImmediate(ref state, bus);
                break;
            case 0xF6:
                OrImmediate(ref state, bus);
                break;
            case 0xFE:
                CpImmediate(ref state, bus);
                break;

            // 8-bit LSM
            case 0xE0 or 0xE2 or 0xEA:
            case 0xF0 or 0xF2 or 0xFA:
                Load8Block3(ref state, bus);
                break;

            default:
                throw new NotImplementedException($"Unexpected opcode {state.Ir}");
        }
    }

    #region 8-Bit ALU

    private static void Add(ref CpuState state, IBus bus) =>
        AddToAInternal(ref state, GetR8(ref state, bus, (byte)(state.Ir & 0b_0111)));

    private static void AddImmediate(ref CpuState state, IBus bus) =>
        AddToAInternal(ref state, Immediate8(ref state, bus));

    private static void Adc(ref CpuState state, IBus bus) =>
        AddToAInternal(ref state, GetR8(ref state, bus, (byte)(state.Ir & 0b_0111)), state.CarryFlag);

    private static void AdcImmediate(ref CpuState state, IBus bus) =>
        AddToAInternal(ref state, Immediate8(ref state, bus), state.CarryFlag);

    private static void AddToAInternal(ref CpuState state, int value, byte carry = 0)
    {
        int result = state.A + value + carry;

        state.ClearFlags();
        state.SetHalfCarryFlag((state.A & 0xF) + (value & 0xF) + carry > 0xF);
        state.SetCarryFlag(result > 0xFF);

        // Set zero flag after down-casting
        state.A = (byte)result;
        state.SetZeroFlag(state.A);
    }

    private static void Sub(ref CpuState state, IBus bus) =>
        state.A = SubFromAInternal(ref state, GetR8(ref state, bus, (byte)(state.Ir & 0b_0111)));

    private static void SubImmediate(ref CpuState state, IBus bus) =>
        state.A = SubFromAInternal(ref state, Immediate8(ref state, bus));

    private static void Sbc(ref CpuState state, IBus bus) =>
        state.A = SubFromAInternal(ref state, GetR8(ref state, bus, (byte)(state.Ir & 0b_0111)), state.CarryFlag);

    private static void SbcImmediate(ref CpuState state, IBus bus) =>
        state.A = SubFromAInternal(ref state, Immediate8(ref state, bus), state.CarryFlag);

    private static void Cp(ref CpuState state, IBus bus)
    {
        uint operand = GetR8(ref state, bus, (byte)(state.Ir & 0b_0111));
        _ = SubFromAInternal(ref state, operand);
    }

    private static void CpImmediate(ref CpuState state, IBus bus)
    {
        uint operand = Immediate8(ref state, bus);
        _ = SubFromAInternal(ref state, operand);
    }

    private static byte SubFromAInternal(ref CpuState state, uint value, byte carry = 0)
    {
        uint result = state.A - value - carry;

        state.ClearFlags();
        state.SetZeroFlag((byte)result);
        state.SetN();
        state.SetHalfCarryFlag((state.A & 0xF) < (value & 0xF) + carry);
        state.SetCarryFlag(result > 0xFF);

        return (byte)result;
    }

    private static void And(ref CpuState state, IBus bus) =>
        AndInternal(ref state, bus, GetR8(ref state, bus, (byte)(state.Ir & 0b_0111)));

    private static void AndImmediate(ref CpuState state, IBus bus) =>
        AndInternal(ref state, bus, Immediate8(ref state, bus));

    private static void AndInternal(ref CpuState state, IBus bus, byte operand)
    {
        state.A = (byte)(state.A & operand);
        state.ClearFlags();
        state.SetHalfCarryFlag(true);
        state.SetZeroFlag(state.A);
    }

    private static void Xor(ref CpuState state, IBus bus) =>
        XorInternal(ref state, bus, GetR8(ref state, bus, (byte)(state.Ir & 0b_0111)));

    private static void XorImmediate(ref CpuState state, IBus bus) =>
        XorInternal(ref state, bus, Immediate8(ref state, bus));

    private static void XorInternal(ref CpuState state, IBus bus, byte operand)
    {
        state.A = (byte)(state.A ^ operand);
        state.ClearFlags();
        state.SetZeroFlag(state.A);
    }

    private static void Or(ref CpuState state, IBus bus) =>
        OrInternal(ref state, bus, GetR8(ref state, bus, (byte)(state.Ir & 0b_0111)));

    private static void OrImmediate(ref CpuState state, IBus bus) =>
        OrInternal(ref state, bus, Immediate8(ref state, bus));

    private static void OrInternal(ref CpuState state, IBus bus, byte operand)
    {
        state.A = (byte)(state.A | operand);
        state.ClearFlags();
        state.SetZeroFlag(state.A);
    }

    #endregion

    #region 16-Bit LSM

    private static void Push(ref CpuState state, IBus bus)
    {
        byte target = (byte)((state.Ir & 0b_0011_0000) >> 4);
        ushort value = GetR16Stk(ref state, bus, target);
        bus[--state.Sp] = Hi(value);
        bus[--state.Sp] = Lo(value);
        state.IncrementMCycles(2);
    }

    private static void Pop(ref CpuState state, IBus bus)
    {
        byte target = (byte)((state.Ir & 0b_0011_0000) >> 4);
        byte lsb = bus[state.Sp++];
        byte msb = bus[state.Sp++];
        ushort value = (ushort)((msb << 8) | lsb);
        state.IncrementMCycles();
        SetR16Stk(ref state, target, value);
    }

    private static ushort GetR16Stk(ref CpuState state, IBus bus, byte target)
    {
        state.IncrementMCycles();
        switch (target)
        {
            case 0:
                return state.BC;
            case 1:
                return state.DE;
            case 2:
                return state.HL;
            default:
            case 3:
                return state.AF;
        }
    }

    private static void SetR16Stk(ref CpuState state, byte target, ushort value)
    {
        state.IncrementMCycles();
        switch (target)
        {
            case 0:
                state.BC = value;
                break;
            case 1:
                state.DE = value;
                break;
            case 2:
                state.HL = value;
                break;
            default:
            case 3:
                state.AF = value;
                break;
        }
    }


    private static void Load16(ref CpuState state, IBus bus)
    {
        byte target = (byte)((state.Ir & 0b_00110000) >> 4);
        SetR16(ref state, bus, target, Immediate16(ref state, bus));
    }

    private static void Load16ToMemory(ref CpuState state, IBus bus)
    {
        ushort nn = Immediate16(ref state, bus);
        bus[nn++] = Lo(state.Sp);
        bus[nn] = Hi(state.Sp);
        state.IncrementMCycles(2);
    }

    private static void Load16SpHl(ref CpuState state)
    {
        state.Sp = state.HL;
        state.IncrementMCycles();
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
            default:
            case 0b_1010:
            {
                // LD A <- [R16]
                byte source = (byte)((state.Ir & 0b_0011_0000) >> 4);
                byte value = GetR16Mem(ref state, bus, source);
                SetR8(ref state, bus, value, Target.A);
                break;
            }
        }
    }

    #endregion

    #region 8-Bit LSM

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
        state.IncrementMCycles();
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
            default:
                state.A = bus[Immediate16(ref state, bus)];
                break;
        }
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

    private static void SetR8(ref CpuState state, IBus bus, byte value, byte target)
    {
        switch (target)
        {
            case Target.B:
                state.B = value;
                break;
            case Target.C:
                state.C = value;
                break;
            case Target.D:
                state.D = value;
                break;
            case Target.E:
                state.E = value;
                break;
            case Target.H:
                state.H = value;
                break;
            case Target.L:
                state.L = value;
                break;
            case Target.HLIndirect: // Special case for indirect HL
                IndirectSet(ref state, bus, state.HL, value);
                break;
            default: // 7
                state.A = value;
                break;
        }
    }

    #endregion

    private static byte GetR16Mem(ref CpuState state, IBus bus, byte target)
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

    private static void SetR16(ref CpuState state, IBus bus, byte target, ushort value)
    {
        switch (target)
        {
            case 0:
                state.BC = value;
                break;
            case 1:
                state.DE = value;
                break;
            case 2:
                state.HL = value;
                break;
            default:
            case 3:
                state.Sp = value;
                break;
        }
    }

    private static void SetR16Mem(ref CpuState state, IBus bus, byte target, byte value)
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
            default:
            case 3:
                bus[state.HL--] = value;
                break;
        }
    }

    private static byte Immediate8(ref CpuState state, IBus bus)
    {
        state.IncrementMCycles();
        return bus[state.Pc++];
    }

    private static ushort Immediate16(ref CpuState state, IBus bus)
    {
        state.IncrementMCycles(2);
        byte lsb = bus[state.Pc++];
        byte msb = bus[state.Pc++];
        return (ushort)((msb << 8) | lsb);
    }

    private static byte IndirectGet(ref CpuState state, IBus bus, ushort address)
    {
        state.IncrementMCycles();
        return bus[address];
    }

    private static void IndirectSet(ref CpuState state, IBus bus, ushort address, byte value)
    {
        state.IncrementMCycles();
        bus[address] = value;
    }

    private static byte Hi(ushort nn) => (byte)((nn & 0xFF00) >> 8);

    private static byte Lo(ushort nn) => (byte)(nn & 0x00FF);

    private static void Panic(byte opcode) =>
        throw new ArgumentException($"Unexpected opcode ${opcode:X2}");
}
