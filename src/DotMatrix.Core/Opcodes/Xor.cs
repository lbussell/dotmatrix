namespace DotMatrix.Core.Opcodes;

[Opcode(0xA8, CpuRegister.B)]
[Opcode(0xA9, CpuRegister.C)]
[Opcode(0xAA, CpuRegister.D)]
[Opcode(0xAB, CpuRegister.E)]
[Opcode(0xAC, CpuRegister.H)]
[Opcode(0xAD, CpuRegister.L)]
[Opcode(0xAF, CpuRegister.A)]
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
