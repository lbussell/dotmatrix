namespace DotMatrix.Core.Opcodes;

public class NotImplemented : IOpcode
{
    public string Name => "Not Implemented";

    public int TCycles => 4;

    public ReadType ReadType => ReadType.None;

    public CpuState Execute(CpuState cpuState, ushort? arg)
    {
        return cpuState;
    }
}
