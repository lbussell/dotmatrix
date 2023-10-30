namespace DotMatrix.Core;

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Explicit)]
public struct CpuState
{
    /*
        Flag explanations:
        Bit 7 = z, Zero flag.
        Bit 6 = n, Subtraction flag (BCD).
        Bit 5 = h, Half Carry flag (BCD).
        Bit 4 = c, Carry flag.
    */
    [FieldOffset(0)]
    public ushort AF;
    [FieldOffset(0)]
    public byte A;
    [FieldOffset(1)]
    public byte F;

    [FieldOffset(2)]
    public ushort BC;
    [FieldOffset(2)]
    public byte B;
    [FieldOffset(3)]
    public byte C;

    [FieldOffset(4)]
    public ushort DE;
    [FieldOffset(4)]
    public byte D;
    [FieldOffset(5)]
    public byte E;

    [FieldOffset(6)]
    public ushort HL;
    [FieldOffset(6)]
    public byte H;
    [FieldOffset(7)]
    public byte L;

    [FieldOffset(8)]
    public ushort SP;
    [FieldOffset(10)]
    public ushort PC;

    public readonly bool Z => (F & 0b1000_0000) != 0;

    public readonly bool N => (F & 0b0100_0000) != 0;

    public readonly bool HalfCarryFlag => (F & 0b0010_0000) != 0;

    public readonly bool CarryFlag => (F & 0b0001_0000) != 0;

    // Maybe: F |= 0b0000_1111;
    public void ClearFlags() => F = 0;

    public void SetZ() => F |= 0b1000_0000;

    public void SetZ(bool value) => F = value ? (byte)(F | 0b1000_0000) : (byte)(F & 0b0111_1111);

    public void ClearZ() => F &= 0b0111_1111;

    public void ClearSetZ() => F = 0b1000_0000;

    public void ToggleZ() => F ^= 0b1000_0000;

    public void SetN() => F |= 0b0100_0000;

    public void SetN(bool value) => F = value ? (byte)(F | 0b0100_0000) : (byte)(F & 0b1011_1111);

    public void ClearN() => F &= 0b1011_1111;

    public void ToggleN() => F ^= 0b0100_0000;

    public void SetHalfCarryFlag() => F |= 0b0010_0000;

    public void SetHalfCarryFlag(bool value) => F = value ? (byte)(F | 0b0010_0000) : (byte)(F & 0b1101_1111);

    public void ClearHalfCarryFlag() => F &= 0b1101_1111;

    public void ToggleHalfCarryFlag() => F ^= 0b0010_0000;

    public void SetCarryFlag() => F |= 0b0001_0000;

    public void SetCarryFlag(bool value) => F = value ? (byte)(F | 0b0001_0000) : (byte)(F & 0b1110_1111);

    public void ClearCarryFlag() => F &= 0b1110_1111;

    public void ToggleCarryFlag() => F ^= 0b0001_0000;
}
