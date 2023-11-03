namespace DotMatrix.Core;

using DotMatrix.Core.Opcodes;

internal sealed partial class Cpu
{
    private static Instruction[] CreateEmptyInstructionsTemp() =>
        Enumerable.Range(0, 256).Select<int, Instruction>(op => () => Control.NotImplemented((byte)op)).ToArray();

    private Instruction[] CreateInstructions()
    {
        Instruction[] i = CreateEmptyInstructionsTemp();

        i[0x01] = () => Load16.Immediate(_bus, ref _cpuState.BC, ref _cpuState.PC);

        i[0x11] = () => Load16.Immediate(_bus, ref _cpuState.DE, ref _cpuState.PC);

        i[0x21] = () => Load16.Immediate(_bus, ref _cpuState.HL, ref _cpuState.PC);
        i[0x22] = () => Load8.FromIndirectInc(ref _cpuState.HL, ref _cpuState.A, _bus);

        i[0x2A] = () => Load8.IndirectInc(ref _cpuState.A, ref _cpuState.HL, _bus);

        i[0x31] = () => Load16.Immediate(_bus, ref _cpuState.SP, ref _cpuState.PC);
        i[0x32] = () => Load8.FromIndirectDec(ref _cpuState.HL, ref _cpuState.A, _bus);

        i[0x3A] = () => Load8.IndirectDec(ref _cpuState.A, ref _cpuState.HL, _bus);

        i[0x40] = () => Load8.Register(ref _cpuState.B, ref _cpuState.B);
        i[0x41] = () => Load8.Register(ref _cpuState.B, ref _cpuState.C);
        i[0x42] = () => Load8.Register(ref _cpuState.B, ref _cpuState.D);
        i[0x43] = () => Load8.Register(ref _cpuState.B, ref _cpuState.E);
        i[0x44] = () => Load8.Register(ref _cpuState.B, ref _cpuState.H);
        i[0x45] = () => Load8.Register(ref _cpuState.B, ref _cpuState.L);

        i[0x47] = () => Load8.Register(ref _cpuState.B, ref _cpuState.A);

        i[0xF9] = () => Load16.SPFromHL(ref _cpuState);

        i[0xA8] = () => Alu8.Xor(ref _cpuState, ref _cpuState.B);
        i[0xA9] = () => Alu8.Xor(ref _cpuState, ref _cpuState.C);
        i[0xAA] = () => Alu8.Xor(ref _cpuState, ref _cpuState.D);
        i[0xAB] = () => Alu8.Xor(ref _cpuState, ref _cpuState.E);
        i[0xAC] = () => Alu8.Xor(ref _cpuState, ref _cpuState.H);
        i[0xAD] = () => Alu8.Xor(ref _cpuState, ref _cpuState.L);
        i[0xAE] = () => Alu8.XorHLIndirect(_bus, ref _cpuState);
        i[0xAF] = () => Alu8.Xor(ref _cpuState, ref _cpuState.A);

        i[0xEE] = () => Alu8.XorImmediate(_bus, ref _cpuState);

        return i;
    }

    private Instruction[] CreatePrefixInstructions()
    {
        Instruction[] i = CreateEmptyInstructionsTemp();

        i[0x7C] = () => Bitwise.Bit(0x07, ref _cpuState.H, ref _cpuState);

        return i;
    }
}