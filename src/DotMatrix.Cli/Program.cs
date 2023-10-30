namespace DotMatrix.Cli;

using DotMatrix.Core;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"Loading Boot ROM from {args[0]}");

        DotMatrixConsole console = new(bootRomPath: args[0]);

        console.TempExecute();
    }
}
