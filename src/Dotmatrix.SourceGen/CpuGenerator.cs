
namespace Dotmatrix.SourceGen;

using System.Collections.Immutable;
using System.Text;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Dotmatrix.SourceGen.Builders;
using Dotmatrix.SourceGen.Model;
using System.Runtime.Serialization;
using System.Diagnostics;

[Generator]
public class CpuGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static postInitializationContext =>
            postInitializationContext.AddSource(
                Names.GeneratedNamespace + Names.GeneratedExtension,
                ClassGenerator.CreateAttribute(Names.GeneratedNamespace, Names.GeneratedAttribute)));

        IncrementalValueProvider<ImmutableArray<DmgOps>> opcodesPipeline =
            context.AdditionalTextsProvider
                .Where(static (text) => text.Path.EndsWith(".json"))
                .Select(static (text, ct) => GetInstructions(text, ct))
                .Collect();

        IncrementalValuesProvider<MethodPath> generatedAttributePipeline =
            context.SyntaxProvider.ForAttributeWithMetadataName(
                fullyQualifiedMetadataName: $"{Names.GeneratedNamespace}.{Names.GeneratedAttribute}",
                predicate: static (syntaxNode, cancellationToken) => syntaxNode is BaseMethodDeclarationSyntax,
                transform: static (context, cancellationToken) =>
                {
                    var containingClass = context.TargetSymbol.ContainingType;
                    return new MethodPath(
                        Namespace: containingClass.ContainingNamespace?.ToDisplayString(
                            SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(
                                SymbolDisplayGlobalNamespaceStyle.Omitted))
                                    ?? "",
                        ClassName: containingClass.Name,
                        MethodName: context.TargetSymbol.Name);
                });

        var instructionPipeline = generatedAttributePipeline.Combine(opcodesPipeline)
            .Select((pair, _) => GetInstructionData(pair));

        context.RegisterSourceOutput(instructionPipeline, InstructionsGenerator.GenerateInstructions);
    }

    private static InstructionsData GetInstructionData((MethodPath MethodPath, ImmutableArray<DmgOps> Instructions) pair)
    {
        if (pair.Instructions.Length > 1)
        {
            throw new InvalidDataException("Found more than one opcode json file. Only one input is supported");
        }

        return new InstructionsData(pair.MethodPath, pair.Instructions.First());
    }

    private static DmgOps GetInstructions(AdditionalText text, CancellationToken ct) =>
        JsonSerializer.Deserialize<DmgOps>(text.GetText(ct)?.ToString() ?? "")
            ?? throw new SerializationException("Got null when deserializing instructions.");
}
