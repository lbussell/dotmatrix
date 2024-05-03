namespace DotMatrix.SourceGen;

using Microsoft.CodeAnalysis;
using DotMatrix.SourceGen.Builders;
using DotMatrix.SourceGen.Model;

internal static class InstructionsGenerator
{
    public static void GenerateInstructions(SourceProductionContext ctx, InstructionsData data) =>
        ctx.AddSource(data.MethodPath.ClassName + Names.GeneratedExtension, GenerateSource(data));

    private static string GenerateSource(InstructionsData data)
    {
        CsharpBuilder builder = new CsharpBuilder().WithNamespace(data.MethodPath.Namespace);

        builder = builder
            .OpenScope($"public partial class {data.MethodPath.ClassName}")
            .OpenScope($"private static partial ValueTuple<CpuState, ExternalState> {data.MethodPath.MethodName}(CpuState cpuState, ExternalState externalState)");

        builder.AppendLine();
        builder.AppendLine("// Regular instructions");
        foreach (Opcode opcode in data.Instructions.Unprefixed)
        {
            builder.AppendLine($"// {opcode.Name}");
        }

        builder.AppendLine();
        builder.AppendLine("// Prefixed instructions");
        foreach (Opcode opcode in data.Instructions.CBPrefixed)
        {
            builder.AppendLine($"// {opcode.Name}");
        }

        builder.AppendLine();
        builder.AppendLine("return (cpuState, externalState);");
        return builder.Build();
    }
}
