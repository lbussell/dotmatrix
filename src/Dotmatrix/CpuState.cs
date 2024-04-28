namespace Dotmatrix;

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Explicit)]
public record struct CpuState()
{
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

    /// <summary>
    /// Stack Pointer
    /// </summary>
    [FieldOffset(8)]
    public ushort SP;

    /// <summary>
    /// Program Counter
    /// </summary>
    [FieldOffset(10)]
    public ushort PC;

    /// <summary>
    /// Interrupt Master Enable flag
    /// </summary>
    [FieldOffset(12)]
    public bool IME;
}
