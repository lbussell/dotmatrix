namespace DotMatrix.Cli;

using System;
using DotMatrix.Core;

internal class Program
{
    static void Main(string[] args)
    {
        // Temporary. Instantiate parts of the console here.

        Console.WriteLine($"Loading Boot ROM from {args[0]}");
        BootRom bootRom = new BootRom(File.ReadAllBytes(args[0]));

        Console.WriteLine($"Loading ROM from {args[1]}");
        Cartridge cartridge = new Cartridge(File.ReadAllBytes(args[1]));

        Memory memory = new();
        Bus bus = new(memory, cartridge, bootRom);
        Cpu cpu = new(bus);

        for (int i = 0; i < MemoryRegion.BootRomEnd / DotMatrixConsoleSpecs.InstructionSizeInBytes; i += 1)
        {
            cpu.ExecuteCycle();
        }
    }
}
