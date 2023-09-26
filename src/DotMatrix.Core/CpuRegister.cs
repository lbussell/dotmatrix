namespace DotMatrix.Core;

public enum CpuRegister
{
    /*
     * Nullable types aren't allowed as Custom Attribute parameters, so we need a default value for the target register
     * that the OpcodeAttribute can have by default.
     */
    Implied,

    AF,
    A,
    F,

    BC,
    B,
    C,

    DE,
    D,
    E,

    HL,
    H,
    L,

    SP,
    PC,
}
