namespace DotMatrix.Core;

using DotMatrix.Core.Opcodes;

internal sealed partial class Cpu
{
    private static Instruction[] CreateEmptyInstructionsTemp() =>
        Enumerable.Range(0, 256).Select<int, Instruction>(op => () => Control.NotImplemented((byte)op)).ToArray();

    private Instruction[] CreateInstructions()
    {
        /* Reference: https://izik1.github.io/gbops/ */

        Instruction[] i = CreateEmptyInstructionsTemp();

        i[0x01] = () => Load16.Immediate(ref _cpuState.BC, ref _cpuState.PC, _bus);
        i[0x04] = () => Alu8.Inc(ref _cpuState.B, ref _cpuState);
        i[0x06] = () => Load8.RegisterImmediate(ref _cpuState.B, ref _cpuState.PC, _bus);
        i[0x0A] = () => Load8.RegisterIndirect(ref _cpuState.A, ref _cpuState.BC, _bus);
        i[0x0C] = () => Alu8.Inc(ref _cpuState.C, ref _cpuState);
        i[0x0E] = () => Load8.RegisterImmediate(ref _cpuState.C, ref _cpuState.PC, _bus);

        i[0x11] = () => Load16.Immediate(ref _cpuState.DE, ref _cpuState.PC, _bus);
        i[0x14] = () => Alu8.Inc(ref _cpuState.D, ref _cpuState);
        i[0x16] = () => Load8.RegisterImmediate(ref _cpuState.D, ref _cpuState.PC, _bus);
        i[0x1A] = () => Load8.RegisterIndirect(ref _cpuState.A, ref _cpuState.DE, _bus);
        i[0x1C] = () => Alu8.Inc(ref _cpuState.E, ref _cpuState);
        i[0x1E] = () => Load8.RegisterImmediate(ref _cpuState.E, ref _cpuState.PC, _bus);

        i[0x20] = () => Branch.RelativeJump(ref _cpuState, _bus, () => !_cpuState.Z); // JR NZ,i8
        i[0x21] = () => Load16.Immediate(ref _cpuState.HL, ref _cpuState.PC, _bus);
        i[0x22] = () => Load8.FromIndirectInc(ref _cpuState.HL, ref _cpuState.A, _bus);
        i[0x24] = () => Alu8.Inc(ref _cpuState.H, ref _cpuState);
        i[0x26] = () => Load8.RegisterImmediate(ref _cpuState.H, ref _cpuState.PC, _bus);
        i[0x2A] = () => Load8.IndirectInc(ref _cpuState.A, ref _cpuState.HL, _bus);
        i[0x2C] = () => Alu8.Inc(ref _cpuState.L, ref _cpuState);
        i[0x2E] = () => Load8.RegisterImmediate(ref _cpuState.L, ref _cpuState.PC, _bus);

        i[0x31] = () => Load16.Immediate(ref _cpuState.SP, ref _cpuState.PC, _bus);
        i[0x32] = () => Load8.FromIndirectDec(ref _cpuState.HL, ref _cpuState.A, _bus);
        i[0x3A] = () => Load8.IndirectDec(ref _cpuState.A, ref _cpuState.HL, _bus);
        i[0x3C] = () => Alu8.Inc(ref _cpuState.A, ref _cpuState);
        i[0x3E] = () => Load8.RegisterImmediate(ref _cpuState.A, ref _cpuState.PC, _bus);

        i[0x40] = () => Load8.Register(ref _cpuState.B, ref _cpuState.B);
        i[0x41] = () => Load8.Register(ref _cpuState.B, ref _cpuState.C);
        i[0x42] = () => Load8.Register(ref _cpuState.B, ref _cpuState.D);
        i[0x43] = () => Load8.Register(ref _cpuState.B, ref _cpuState.E);
        i[0x44] = () => Load8.Register(ref _cpuState.B, ref _cpuState.H);
        i[0x45] = () => Load8.Register(ref _cpuState.B, ref _cpuState.L);
        i[0x46] = () => Load8.RegisterIndirect(ref _cpuState.B, ref _cpuState.HL, _bus);
        i[0x47] = () => Load8.Register(ref _cpuState.B, ref _cpuState.A);
        i[0x48] = () => Load8.Register(ref _cpuState.C, ref _cpuState.B);
        i[0x49] = () => Load8.Register(ref _cpuState.C, ref _cpuState.C);
        i[0x4A] = () => Load8.Register(ref _cpuState.C, ref _cpuState.D);
        i[0x4B] = () => Load8.Register(ref _cpuState.C, ref _cpuState.E);
        i[0x4C] = () => Load8.Register(ref _cpuState.C, ref _cpuState.H);
        i[0x4D] = () => Load8.Register(ref _cpuState.C, ref _cpuState.L);
        i[0x4E] = () => Load8.RegisterIndirect(ref _cpuState.C, ref _cpuState.HL, _bus);
        i[0x4F] = () => Load8.Register(ref _cpuState.C, ref _cpuState.A);

        i[0x50] = () => Load8.Register(ref _cpuState.D, ref _cpuState.B);
        i[0x51] = () => Load8.Register(ref _cpuState.D, ref _cpuState.C);
        i[0x52] = () => Load8.Register(ref _cpuState.D, ref _cpuState.D);
        i[0x53] = () => Load8.Register(ref _cpuState.D, ref _cpuState.E);
        i[0x54] = () => Load8.Register(ref _cpuState.D, ref _cpuState.H);
        i[0x55] = () => Load8.Register(ref _cpuState.D, ref _cpuState.L);
        i[0x56] = () => Load8.RegisterIndirect(ref _cpuState.D, ref _cpuState.HL, _bus);
        i[0x57] = () => Load8.Register(ref _cpuState.D, ref _cpuState.A);
        i[0x58] = () => Load8.Register(ref _cpuState.E, ref _cpuState.B);
        i[0x59] = () => Load8.Register(ref _cpuState.E, ref _cpuState.C);
        i[0x5A] = () => Load8.Register(ref _cpuState.E, ref _cpuState.D);
        i[0x5B] = () => Load8.Register(ref _cpuState.E, ref _cpuState.E);
        i[0x5C] = () => Load8.Register(ref _cpuState.E, ref _cpuState.H);
        i[0x5D] = () => Load8.Register(ref _cpuState.E, ref _cpuState.L);
        i[0x5E] = () => Load8.RegisterIndirect(ref _cpuState.E, ref _cpuState.HL, _bus);
        i[0x5F] = () => Load8.Register(ref _cpuState.E, ref _cpuState.A);

        i[0x60] = () => Load8.Register(ref _cpuState.H, ref _cpuState.B);
        i[0x61] = () => Load8.Register(ref _cpuState.H, ref _cpuState.C);
        i[0x62] = () => Load8.Register(ref _cpuState.H, ref _cpuState.D);
        i[0x63] = () => Load8.Register(ref _cpuState.H, ref _cpuState.E);
        i[0x64] = () => Load8.Register(ref _cpuState.H, ref _cpuState.H);
        i[0x65] = () => Load8.Register(ref _cpuState.H, ref _cpuState.L);
        i[0x66] = () => Load8.RegisterIndirect(ref _cpuState.H, ref _cpuState.HL, _bus);
        i[0x67] = () => Load8.Register(ref _cpuState.H, ref _cpuState.A);
        i[0x68] = () => Load8.Register(ref _cpuState.L, ref _cpuState.B);
        i[0x69] = () => Load8.Register(ref _cpuState.L, ref _cpuState.C);
        i[0x6A] = () => Load8.Register(ref _cpuState.L, ref _cpuState.D);
        i[0x6B] = () => Load8.Register(ref _cpuState.L, ref _cpuState.E);
        i[0x6C] = () => Load8.Register(ref _cpuState.L, ref _cpuState.H);
        i[0x6D] = () => Load8.Register(ref _cpuState.L, ref _cpuState.L);
        i[0x6E] = () => Load8.RegisterIndirect(ref _cpuState.L, ref _cpuState.HL, _bus);
        i[0x6F] = () => Load8.Register(ref _cpuState.L, ref _cpuState.A);

        i[0x70] = () => Load8.FromRegisterIndirectHL(ref _cpuState.B, ref _cpuState, _bus);
        i[0x71] = () => Load8.FromRegisterIndirectHL(ref _cpuState.C, ref _cpuState, _bus);
        i[0x72] = () => Load8.FromRegisterIndirectHL(ref _cpuState.D, ref _cpuState, _bus);
        i[0x73] = () => Load8.FromRegisterIndirectHL(ref _cpuState.E, ref _cpuState, _bus);
        i[0x74] = () => Load8.FromRegisterIndirectHL(ref _cpuState.H, ref _cpuState, _bus);
        i[0x75] = () => Load8.FromRegisterIndirectHL(ref _cpuState.L, ref _cpuState, _bus);
        /* i[0x76] = halt */
        i[0x77] = () => Load8.FromRegisterIndirectHL(ref _cpuState.A, ref _cpuState, _bus);
        i[0x78] = () => Load8.Register(ref _cpuState.A, ref _cpuState.B);
        i[0x79] = () => Load8.Register(ref _cpuState.A, ref _cpuState.C);
        i[0x7A] = () => Load8.Register(ref _cpuState.A, ref _cpuState.D);
        i[0x7B] = () => Load8.Register(ref _cpuState.A, ref _cpuState.E);
        i[0x7C] = () => Load8.Register(ref _cpuState.A, ref _cpuState.H);
        i[0x7D] = () => Load8.Register(ref _cpuState.A, ref _cpuState.L);
        i[0x7E] = () => Load8.RegisterIndirect(ref _cpuState.A, ref _cpuState.HL, _bus);
        i[0x7F] = () => Load8.Register(ref _cpuState.A, ref _cpuState.A);

        i[0xF9] = () => Load16.SPFromHL(ref _cpuState);

        i[0xA8] = () => Alu8.Xor(ref _cpuState, ref _cpuState.B);
        i[0xA9] = () => Alu8.Xor(ref _cpuState, ref _cpuState.C);
        i[0xAA] = () => Alu8.Xor(ref _cpuState, ref _cpuState.D);
        i[0xAB] = () => Alu8.Xor(ref _cpuState, ref _cpuState.E);
        i[0xAC] = () => Alu8.Xor(ref _cpuState, ref _cpuState.H);
        i[0xAD] = () => Alu8.Xor(ref _cpuState, ref _cpuState.L);
        i[0xAE] = () => Alu8.XorHLIndirect(_bus, ref _cpuState);
        i[0xAF] = () => Alu8.Xor(ref _cpuState, ref _cpuState.A);

        i[0xE0] = () => Load8.FromADirect(ref _cpuState, ref _cpuState.PC, _bus);
        i[0xE2] = () => Load8.FromAIndirect(ref _cpuState, ref _cpuState.PC, _bus);
        i[0xEE] = () => Alu8.XorImmediate(_bus, ref _cpuState);

        return i;
    }

    private Instruction[] CreatePrefixInstructions()
    {
        Instruction[] i = CreateEmptyInstructionsTemp();

        int addr = 0x40;
        for (int bit = 0; bit <= 7; bit += 1)
        {
            i[addr + 0] = () => Bitwise.Bit((byte)bit, ref _cpuState.B, ref _cpuState);
            i[addr + 1] = () => Bitwise.Bit((byte)bit, ref _cpuState.C, ref _cpuState);
            i[addr + 2] = () => Bitwise.Bit((byte)bit, ref _cpuState.D, ref _cpuState);
            i[addr + 3] = () => Bitwise.Bit((byte)bit, ref _cpuState.E, ref _cpuState);
            i[addr + 4] = () => Bitwise.Bit((byte)bit, ref _cpuState.H, ref _cpuState);
            i[addr + 5] = () => Bitwise.Bit((byte)bit, ref _cpuState.L, ref _cpuState);
            i[addr + 7] = () => Bitwise.Bit((byte)bit, ref _cpuState.A, ref _cpuState);
            addr += 0x08;
        }

        return i;
    }
}
