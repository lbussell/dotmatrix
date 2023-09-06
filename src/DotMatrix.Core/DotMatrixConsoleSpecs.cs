namespace DotMatrix.Core;

public static class DotMatrixConsoleSpecs
{
    public static Vec2Int DisplaySize { get; } = new() { Y = 144, X = 160 };
    public static int MaxCartridgeSizeInBytes = 0x200000;
}
