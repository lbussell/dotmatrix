namespace DotMatrix.Core.Opcodes;

public sealed class Bit0(CpuRegister r) : Bit(0, r);

public sealed class Bit1(CpuRegister r) : Bit(1, r);

public sealed class Bit2(CpuRegister r) : Bit(2, r);

public sealed class Bit3(CpuRegister r) : Bit(3, r);

public sealed class Bit4(CpuRegister r) : Bit(4, r);

public sealed class Bit5(CpuRegister r) : Bit(5, r);

public sealed class Bit6(CpuRegister r) : Bit(6, r);

[Opcode(0x7C, CpuRegister.H, Prefix = true)]
public sealed class Bit7(CpuRegister r) : Bit(7, r);

public class Bit(int n, CpuRegister r) : IOpcode
{
    public int TCycles => 8;

    public ReadType ReadType => ReadType.None;

    public string Format(string? arg = null)
    {
        return $"BIT {n},{CpuState.Name(r)}";
    }
}
