namespace DotMatrix.Cli;

using DotMatrix;

internal class Program
{
    private static void Main(string? bios, string? cart)
    {
        if (bios is null && cart is null)
        {
            throw new ArgumentException("Must have at least one of either bios or cart to start.");
        }

        byte[]? biosData = bios is not null ? File.ReadAllBytes(Path.GetFullPath(bios)) : null;
        byte[]? cartData = cart is not null ? File.ReadAllBytes(Path.GetFullPath(cart)) : null;

        Console.WriteLine(biosData);

        IGameConsole console = GameConsoleFactory.Create(biosData, cartData);
        console.Start();
    }
}
