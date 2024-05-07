namespace DotMatrix.SourceGen;

using System.Collections.Immutable;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.Serialization;
using DotMatrix.SourceGen.Model;
using DotMatrix.SourceGen.Model.Generator;

[Generator]
public class CpuSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static postInitializationContext =>
            postInitializationContext.AddSource(
                Names.GeneratedNamespace + Names.GeneratedExtension,
                CsharpHelper.CreateAttribute(Names.GeneratedNamespace, Names.GeneratedAttribute)));

        IncrementalValueProvider<ImmutableArray<DmgOps>> opcodesPipeline = context.AdditionalTextsProvider
            .Where(static (text) => text.Path.EndsWith("dmgops.json"))
            .Select(static (text, ct) => SerializeDmgOps(text, ct))
            .Collect();

        IncrementalValuesProvider<MethodInfo> generatedAttributePipeline =
            context.SyntaxProvider.ForAttributeWithMetadataName(
                fullyQualifiedMetadataName: $"{Names.GeneratedNamespace}.{Names.GeneratedAttribute}",
                predicate: static (syntaxNode, cancellationToken) => syntaxNode is BaseMethodDeclarationSyntax,
                transform: static (context, cancellationToken) =>
                {
                    INamedTypeSymbol? containingClass = context.TargetSymbol.ContainingType;
                    return new MethodInfo(
                        Namespace: containingClass.ContainingNamespace?.ToDisplayString(
                            SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(
                                SymbolDisplayGlobalNamespaceStyle.Omitted))
                                    ?? "",
                        ClassName: containingClass.Name,
                        MethodName: context.TargetSymbol.Name);
                });

        IncrementalValuesProvider<InstructionsData> instructionPipeline = generatedAttributePipeline
            .Combine(opcodesPipeline)
            .Select((pair, _) => GetInstructionData(pair));

        context.RegisterSourceOutput(instructionPipeline, InstructionsHelper.GenerateAllInstructions);
    }

    private static InstructionsData GetInstructionData(
        (MethodInfo MethodPath, ImmutableArray<DmgOps> Instructions) pair)
    {
        if (pair.Instructions.Length > 1)
        {
            throw new InvalidDataException(
                "Found more than one opcode json file. Only one input is supported");
        }

        return new InstructionsData(pair.MethodPath, pair.Instructions.First());
    }

    private static DmgOps SerializeDmgOps(AdditionalText text, CancellationToken ct) =>
        JsonSerializer.Deserialize<DmgOps>(text.GetText(ct)?.ToString() ?? "")
            ?? throw new SerializationException("Got null when deserializing instructions.");
}
