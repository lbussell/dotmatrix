namespace DotMatrix.Core;

public record struct CpuState
{
    /**
        Accumulator and flags.
        Flag explanations:
        Bit 7 = z, Zero flag.
        Bit 6 = n, Subtraction flag (BCD).
        Bit 5 = h, Half Carry flag (BCD).
        Bit 4 = c, Carry flag.
    */
    public ushort AF;

    // Registers
    public ushort BC;
    public ushort DE;
    public ushort HL;

    /** Stack pointer. */
    public ushort SP;

    /** Program counter. */
    public ushort PC;
}
