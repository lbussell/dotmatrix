namespace DotMatrix.Core.Opcodes;

[Opcode(0x00)]
public class Nop : IOpcode
{
    public string Name => "NOP";

    public int TCycles => 4;

    public ReadType ReadType => ReadType.None;

    public CpuState Execute(CpuState cpuState, ushort? arg)
    {
        return cpuState;
    }
}
