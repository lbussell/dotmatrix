namespace DotMatrix.Core.Opcodes;

[Opcode(0xA8, R = CpuRegister.B)]
[Opcode(0xA9, R = CpuRegister.C)]
[Opcode(0xAA, R = CpuRegister.D)]
[Opcode(0xAB, R = CpuRegister.E)]
[Opcode(0xAC, R = CpuRegister.H)]
[Opcode(0xAD, R = CpuRegister.L)]
[Opcode(0xAF, R = CpuRegister.A)]
internal sealed class XOR(CpuRegister r) : IInstruction
{
    public string Name => $"{nameof(XOR)} A,{Enum.GetName(typeof(CpuRegister), r)}";

    public int TCycles => 4;

    public CpuState Execute(CpuState cpuState, Bus bus)
    {
        byte result = (byte)(CpuUtil.GetHi(cpuState.AF) ^ CpuUtil.GetRegister(cpuState, r));

        // TODO: make setting flags better...
        byte flags = (byte)(result == 0 ? 0b10000000 : 0x00);

        ushort af = (byte)(CpuUtil.SetHi(cpuState.AF, result) | flags);
        return cpuState with { AF = af };
    }
}
