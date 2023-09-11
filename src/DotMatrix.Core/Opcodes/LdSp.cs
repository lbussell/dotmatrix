namespace DotMatrix.Core.Opcodes;

[Opcode(0x31)]
internal sealed class LdSp : IInstruction
{
    public string Name => "LD SP,u16";

    public int TCycles => 12;

    public CpuState Execute(CpuState cpuState, Bus bus)
    {
        ushort arg = bus.ReadInc16(ref cpuState.PC);
        return cpuState with { SP = arg };
    }
}
