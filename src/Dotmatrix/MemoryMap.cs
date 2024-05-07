namespace DotMatrix;

internal static class MemoryMap
{
    public static class BootRom
    {
        public const int Start = 0x0000;
        public const int End = 0x00FF;
    }

    public static class RomBank00
    {
        public const int Start = 0x0000;
        public const int End = 0x3FFF;
    }

    public static class RomBank01NN
    {
        public const int Start = 0x4000;
        public const int End = 0x7FFF;
    }

    public static class VRam
    {
        public const int Start = 0x8000;
        public const int End = 0x9FFF;
    }

    public static class ExRam
    {
        public const int Start = 0xA000;
        public const int End = 0xBFFF;
    }

    public static class WRam
    {
        public const int Start = 0xC000;
        public const int End = 0xCFFF;
    }

    public static class WRam2
    {
        public const int Start = 0xD000;
        public const int End = 0xDFFF;
    }

    public static class EchoRam
    {
        public const int Start = 0xE000;
        public const int End = 0xFDFF;
    }

    public static class OAM
    {
        public const int Start = 0xFE00;
        public const int End = 0xFE9F;
    }

    public static class Unusable
    {
        public const int Start = 0xFEA0;
        public const int End = 0xFEFF;
    }

    public static class IOReg
    {
        public const int Start = 0xFF00;
        public const int End = 0xFF7F;
    }

    public static class HRam
    {
        public const int Start = 0xFF80;
        public const int End = 0xFFFE;
    }

    public static class IEReg
    {
        public const int Start = 0xFFFF;
        public const int End = 0xFFFF;
    }
}