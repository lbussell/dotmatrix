namespace DotMatrix.Cli;

using System;
using DotMatrix.Core;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"Loading Boot ROM from {args[0]}");
        byte[] bootRom = File.ReadAllBytes(args[0]);

        Console.WriteLine($"Loading ROM from {args[1]}");
        byte[] rom = File.ReadAllBytes(args[1]);

        // Temporary. Instantiate parts of the console here.
        Cartridge cartridge = new Cartridge(rom);
        Memory memory = new();
        Bus bus = new() { Memory = memory, Cartridge = cartridge };
    }
}
