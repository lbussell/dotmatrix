namespace DotMatrix.Cli;

using System;
using DotMatrix.Core;

internal class Program
{
    static void Main(string[] args)
    {
        byte[] rom = File.ReadAllBytes(args[0]);
        Memory memory = new();
        memory.LoadCartridge(rom);
    }
}
