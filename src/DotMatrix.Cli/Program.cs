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

        RenderOpcodeTable();
    }

    private static void RenderOpcodeTable()
    {
        IEnumerable<int> unsupportedOpcodes =
        [
            0xD3, 0xDB, 0xDD, 0xE3, 0xE4, 0xEB, 0xEC, 0xED, 0xF4, 0xFC, 0xFD,
        ];

        bool[] instructionsArray = Util.GetImplementedOpcodesArray().ToArray();
        bool[] anticipatedArray = Util.GetAnticipatedOpcodesArray().ToArray();
        string[] zippedArray = instructionsArray
            .Zip(anticipatedArray)
            .Select((t, i) => unsupportedOpcodes.Contains(i) ? "X" : (t.First ? "Impl" : t.Second ? "..." : " "))
            .ToArray();

        if (instructionsArray.Length != 0x100)
        {
            throw new ArgumentException("Expected exactly 0x100 instructions.");
        }

        Table table = new();

        table.AddColumn("----");
        for (int lo = 0; lo < 0x10; lo += 0x01)
        {
            table.AddColumn($"+{lo:X1}");
        }

        for (int hi = 0x00; hi < 0xFF; hi += 0x10)
        {
            // IEnumerable<string> rows = instructionsArray[hi..(hi + 0x10)]
            //     .Select(b => b ? "Done" : " ");

            IEnumerable<string> rows = zippedArray[hi..(hi + 0x10)];
            rows = [$"0x{hi:X2}+", ..rows];

            table.AddRow(rows.ToArray());
        }

        table.ShowRowSeparators();

        AnsiConsole.Write(table.Expand());
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
