namespace DotMatrix;

public record struct ExternalState(
    int Cycles = 0,
    int CyclesSinceLastFrame = 0,
    int Frames = 0);
