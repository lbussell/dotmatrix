namespace DotMatrix.Core.Opcodes;

[Opcode(0x01, R = CpuRegister.BC)]
[Opcode(0x11, R = CpuRegister.DE)]
[Opcode(0x21, R = CpuRegister.HL)]
[Opcode(0x31, R = CpuRegister.SP)]
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
