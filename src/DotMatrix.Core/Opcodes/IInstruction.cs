namespace DotMatrix.Core.Opcodes;

internal interface IInstruction
{
    public string Name { get; }

    public int TCycles { get; }

    public CpuState Execute(CpuState cpuState, Bus bus);
}
