namespace DotMatrix.Core.Opcodes;

[Opcode(0x00)]
public class Nop : IInstruction
{
    public string Name => "NOP";

    public int TCycles => 4;

    public CpuState Execute(CpuState cpuState, Bus bus)
    {
        return cpuState;
    }
}
