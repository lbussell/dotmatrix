namespace DotMatrix.Core;

public enum CpuRegister
{
    /*
     * Nullable types aren't allowed as Custom Attribute parameters, so we need a default value for the target register
     * that the OpcodeAttribute can have by default.
     */
    Implied,

    A,
    F,
    B,
    C,
    D,
    E,
    H,
    L,
}
