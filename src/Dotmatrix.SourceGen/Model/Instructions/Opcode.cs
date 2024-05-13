namespace DotMatrix.SourceGen.Model.Instructions;

public record Opcode(
    string Name,
    string Group,
    int TCyclesBranch,
    int TCyclesNoBranch,
    int Length,
    Flags Flags,
    Timing[] TimingNoBranch,
    Timing[]? TimingBranch);
