namespace DotMatrix.Cli;

using System;
using DotMatrix.Core;

class Program
{
    static void Main(FileInfo bios, FileInfo rom)
    {
        Validate(bios, nameof(bios));
        Validate(rom, nameof(rom));

        byte[] biosData = File.ReadAllBytes(bios.FullName);
        Console.WriteLine($"Read bios with length ${biosData.Length:X4}");

        byte[] romData = File.ReadAllBytes(rom.FullName);
        Console.WriteLine($"Read rom with length ${rom.Length:X4}");

        DotMatrixConsole c = DotMatrixConsole.CreateInstance(biosData, romData);
        c.Run();
    }

    private static void Validate(FileInfo fileInfo, string name)
    {
        if (!fileInfo.Exists)
        {
            throw new ArgumentException(name);
        }
    }
}
