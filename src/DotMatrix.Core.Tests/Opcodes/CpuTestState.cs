using DotMatrix.Core.Tests.Model;

namespace DotMatrix.Core.Tests.Opcodes;

public record CpuTestState(CpuState State, int[][] Ram)
{
    public static CpuTestState FromModel(CpuTestStateModel model, int tCycles = 0) =>
        new(
            new CpuState
            {
                A = model.A,
                B = model.B,
                C = model.C,
                D = model.D,
                E = model.E,
                F = model.F,
                H = model.H,
                L = model.L,
                Pc = (ushort)(model.Pc - 1),
                Sp = model.Sp,
                TCycles = (ulong)tCycles,
            },
            model.Ram
        );

    public override string ToString()
    {
        return State.ToString();
    }
}
