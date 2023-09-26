namespace DotMatrix.Cli;

using System;
using DotMatrix.Core;
using DotMatrix.Core.Opcodes;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"Loading Boot ROM from {args[0]}");
        IReadableMemory bootRom = new BootRom(File.ReadAllBytes(args[0]));
        IEnumerable<Instruction> instructions = new Disassembler().Disassemble(bootRom);

        foreach (Instruction inst in instructions)
        {
            Console.WriteLine(inst);
        }

        // Console.WriteLine($"Loading ROM from {args[1]}");
        // Cartridge cartridge = new(File.ReadAllBytes(args[1]));

        // Memory memory = new();
        // Bus bus = new(memory, cartridge, bootRom);
        // IDisplay display = new NoDisplay();
        // Cpu cpu = new(bus, display);

        // for (int i = 0; i < MemoryRegion.BootRomEnd / DotMatrixConsoleSpecs.InstructionSizeInBytes; i += 1)
        // {
        //     cpu.ExecuteCycle();
        // }

        // cpu.ExecuteFrame(MemoryRegion.BootRomEnd * ConsoleSpecs.InstructionSizeInBytes);
    }
}
