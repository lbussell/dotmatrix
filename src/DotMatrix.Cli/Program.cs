namespace DotMatrix.Cli;

using System;
using DotMatrix.Core;
using Spectre.Console;

class Program
{
    static void Main()
    {
        // DotMatrixConsole c = DotMatrixConsole.CreateInstance(biosData, romData);
        // c.Run();

        RenderOpcodeTable(TestOpcodes());
    }

    private static bool[] TestOpcodes()
    {
        bool[] instructionsDone = new bool[256];

        OpcodeHandler handler = new();
        IBus bus = new DummyBus();
        CpuState bogusCpuState = new();

        for (int i = 0; i <= 0xFF; i += 1)
        {
            bogusCpuState.Ir = (byte)i;

            try
            {
                handler.HandleOpcode(ref bogusCpuState, bus);
                instructionsDone[i] = true;
            }
            catch (NotImplementedException e)
            {
                instructionsDone[i] = false;
            }
        }

        return instructionsDone;
    }

    private static void RenderOpcodeTable(bool[] instructions)
    {
        Table table = new()
        {
        };

        table.AddColumn("----");
        for (int lo = 0; lo < 0xFF; lo += 0x10)
        {
            table.AddColumn($"0x{lo:X2}");
        }

        for (int hi = 0x00; hi < 0xFF; hi += 0x10)
        {
            IEnumerable<string> rows = instructions[hi..(hi + 0x10)]
                .Select(b => b ? "Done" : " ");

            rows = [$"0x{hi:X2}+", ..rows];

            table.AddRow(rows.ToArray());
        }

        table.ShowRowSeparators();

        AnsiConsole.Write(table.Expand());
    }

    private class DummyBus : IBus
    {
        public byte this[ushort key]
        {
            get => 0;
            set { }
        }
    }

    // static void Main(FileInfo bios, FileInfo rom)
    // {
    //     Validate(bios, nameof(bios));
    //     Validate(rom, nameof(rom));
    //
    //     byte[] biosData = File.ReadAllBytes(bios.FullName);
    //     Console.WriteLine($"Read bios with length ${biosData.Length:X4}");
    //
    //     byte[] romData = File.ReadAllBytes(rom.FullName);
    //     Console.WriteLine($"Read rom with length ${rom.Length:X4}");
    //
    //     DotMatrixConsole c = DotMatrixConsole.CreateInstance(biosData, romData);
    //     c.Run();
    // }

    // private static void Validate(FileInfo fileInfo, string name)
    // {
    //     if (!fileInfo.Exists)
    //     {
    //         throw new ArgumentException(name);
    //     }
    // }
}
