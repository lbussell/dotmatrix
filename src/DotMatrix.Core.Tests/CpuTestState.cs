namespace DotMatrix.Core.Tests;

public record CpuTestState(
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
    int[][] Ram)
{
    public CpuState ToCpuState() =>
        new CpuState
        {
            A = A,
            B = B,
            C = C,
            D = D,
            E = E,
            F = F,
            H = H,
            L = L,
            Pc = Pc,
            Sp = Sp,
        };

    public Dictionary<ushort, byte> GetTestRam()
    {
        Dictionary<ushort, byte> testRam = [];

        foreach (int[] pairing in Ram)
        {
            testRam[(ushort)pairing[0]] = (byte)pairing[1];
        }

        return testRam;
    }
}
