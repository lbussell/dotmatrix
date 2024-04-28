namespace Dotmatrix;

using Dotmatrix.Generated;

public partial class SM83 : ICpu
{
    [GenerateCpuInstructions]
    partial void Execute();
}
