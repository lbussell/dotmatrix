using Microsoft.CodeAnalysis;
using DotMatrix.SourceGen.Builders;
using DotMatrix.SourceGen.Model;
using DotMatrix.SourceGen.Model.Generator;
using DotMatrix.SourceGen.Model.Instructions;

namespace DotMatrix.SourceGen;

internal static class InstructionsHelper
{
    private const string Bus = "_bus";
    private const string State = "cpuState";
    private const string ExtState = "externalState";
    private const string SP = $"{State}.SP";
    private const string PC = $"{State}.PC";

    private const string MethodSignatureFormatString =
        "private partial ValueTuple<CpuState, ExternalState> {0}(CpuState cpuState, ExternalState externalState)";

    public static void GenerateAllInstructions(SourceProductionContext ctx, InstructionGenerationData data) =>
        ctx.AddSource(data.MethodInfo.ClassName + Names.GeneratedExtension, GenerateAllInstructionsInternal(data));

    private static CsharpBuilder AddOpeningBoilerplate(this CsharpBuilder builder, MethodInfo method) => builder
        .WithNamespace(method.Namespace)
        .OpenScope($"public partial class {method.ClassName}")
        .OpenScope(string.Format(MethodSignatureFormatString, method.MethodName));

    private static CsharpBuilder AddClosingBoilerplate(this CsharpBuilder builder) => builder
        .AppendLine("return (cpuState, externalState);");

    private static string Print(string var, int digits = 2, string format = "X") => $"Console.WriteLine($\"{{{var}:{format}{digits}}}\");";

    private static string Read8(bool inc = false) => $"{Bus}[{PC}{(inc ? "++" : "")}]";

    private static string GenerateAllInstructionsInternal(InstructionGenerationData data)
    {
        CsharpBuilder builder = new CsharpBuilder().AddOpeningBoilerplate(data.MethodInfo);

        builder.AppendLine($"byte opcode = {Read8(inc: true)};");
        builder.AppendLine(Print("opcode"));

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

        return builder.AppendLine().AddClosingBoilerplate().Build();
    }
}
