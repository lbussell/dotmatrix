namespace Dotmatrix.SourceGen;

using Microsoft.CodeAnalysis;

internal static class InstructionsGenerator
{
    public static void GenerateInstructions(SourceProductionContext ctx, InstructionsData data) =>
        ctx.AddSource(data.MethodPath.ClassName + Names.GeneratedExtension, GenerateSource(data));

    public static string GenerateSource(InstructionsData data)
    {
        return "hello!";
    }
}
