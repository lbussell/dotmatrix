namespace DotMatrix.Core;

public static class ConsoleSpecs
{
    public const int MaxCartridgeSizeInBytes = 0x200000;
    public const int CpuSpeed = 4194304;
    public const int FramesPerSecond = 60;
    public const int InstructionSizeInBytes = 2;
    public const int BootRomLengthInBytes = 0x100;
    public const byte Prefix = 0xCB;

    public static Vec2Int DisplaySize { get; } = new() { Y = 144, X = 160 };
}
