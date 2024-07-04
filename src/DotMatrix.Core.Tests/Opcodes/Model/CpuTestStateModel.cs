namespace DotMatrix.Core.Tests.Model;

public record CpuTestStateModel(
    byte A,
    byte B,
    byte C,
    byte D,
    byte E,
    byte F,
    byte H,
    byte L,
    ushort Pc,
    ushort Sp,
    int[][] Ram);
