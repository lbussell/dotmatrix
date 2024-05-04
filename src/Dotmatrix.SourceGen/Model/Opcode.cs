namespace DotMatrix.SourceGen.Model;

public record Opcode(
    string Name,
    string Group,
    int TCyclesBranch,
    int TCyclesNoBranch,
    int Length,
    Flags Flags,
    Timing[] TimingNoBranch,
    Timing[]? TimingBranch);
