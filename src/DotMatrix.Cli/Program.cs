namespace DotMatrix.Cli;

using System;
using DotMatrix.Core;

internal class Program
{
    static void Main(string[] args)
    {
        // Temporary. Instantiate parts of the console here.

        Console.WriteLine($"Loading Boot ROM from {args[0]}");
        BootRom bootRom = new(File.ReadAllBytes(args[0]));

        Console.WriteLine($"Loading ROM from {args[1]}");
        Cartridge cartridge = new(File.ReadAllBytes(args[1]));

        Memory memory = new();
        Bus bus = new(memory, cartridge, bootRom);
        IDisplay display = new NoDisplay();
        Cpu cpu = new(bus, display);

        // for (int i = 0; i < MemoryRegion.BootRomEnd / DotMatrixConsoleSpecs.InstructionSizeInBytes; i += 1)
        // {
        //     cpu.ExecuteCycle();
        // }

        cpu.ExecuteFrame(MemoryRegion.BootRomEnd * DotMatrixConsoleSpecs.InstructionSizeInBytes);
    }
}
