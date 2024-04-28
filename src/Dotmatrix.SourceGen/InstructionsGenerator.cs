using Dotmatrix.SourceGen.Builders;

namespace Dotmatrix.SourceGen;

using Microsoft.CodeAnalysis;

internal static class InstructionsGenerator
{
    public static void GenerateInstructions(SourceProductionContext ctx, InstructionsData data) =>
        ctx.AddSource(data.MethodPath.ClassName + Names.GeneratedExtension, GenerateSource(data));

    public static string GenerateSource(InstructionsData data)
    {
        return new CsharpBuilder()
            .WithNamespace(data.MethodPath.Namespace)
            .OpenScope($"public partial class {data.MethodPath.ClassName}")
            .OpenScope($"private partial CpuState {data.MethodPath.MethodName}(CpuState previousState)")
            .AppendLine("throw new NotImplementedException(\"Not implemented\");")
            .CloseScope()
            .CloseScope()
            .Build();
    }
}
