namespace DotMatrix.Core.Opcodes;

[Opcode(0x01, CpuRegister.BC)]
[Opcode(0x11, CpuRegister.DE)]
[Opcode(0x21, CpuRegister.HL)]
[Opcode(0x31, CpuRegister.SP)]
internal sealed class Ld16(CpuRegister targetRegister) : IOpcode
{
    public string Name => $"LD {CpuState.Name(targetRegister)}";

    public int TCycles => 12;

    public ReadType ReadType => ReadType.Read16;

    public CpuState Execute(CpuState cpuState, ushort? arg)
    {
        cpuState[targetRegister] = arg ?? throw new ArgumentNullException(nameof(arg));
        return cpuState;
    }
}
