namespace DotMatrix.Cli;

using DotMatrix.Core;

class Program
{
    static void Main(string[] args)
    {
        // byte[] bios = File.ReadAllBytes(args[0]);
        byte[] rom = File.ReadAllBytes(args[0]);
        DotMatrixConsole console = DotMatrixConsole.CreateInstance(rom, bios: null, loggingEnabled: true);
        console.Run();
    }
}
