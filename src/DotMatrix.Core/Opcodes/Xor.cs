namespace DotMatrix.Core.Opcodes;

[Opcode(0xA8, R = CpuRegister.B)]
[Opcode(0xA9, R = CpuRegister.C)]
[Opcode(0xAA, R = CpuRegister.D)]
[Opcode(0xAB, R = CpuRegister.E)]
[Opcode(0xAC, R = CpuRegister.H)]
[Opcode(0xAD, R = CpuRegister.L)]
[Opcode(0xAF, R = CpuRegister.A)]
internal sealed class Xor(CpuRegister r) : IOpcode
{
    public string Name => $"XOR A,{Enum.GetName(typeof(CpuRegister), r)}";

    public int TCycles => 4;

    public ReadType ReadType => ReadType.None;

    public CpuState Execute(CpuState cpuState, ushort? arg)
    {
        byte result = (byte)(cpuState.A ^ cpuState[r]);

        // TODO: make setting flags better...
        byte flags = (byte)(result == 0 ? 0b10000000 : 0x00);
        return cpuState with { A = result, F = flags };
    }
}
