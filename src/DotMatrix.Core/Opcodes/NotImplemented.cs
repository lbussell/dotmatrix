namespace DotMatrix.Core.Opcodes;

public class NotImplemented : IInstruction
{
    public string Name => "Not Implemented";

    public int TCycles => 4;

    public CpuState Execute(CpuState cpuState, Bus bus)
    {
        return cpuState;
    }
}
