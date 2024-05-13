using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using DotMatrix.SourceGen.Model.Generator;
using DotMatrix.SourceGen.Model.Instructions;

namespace DotMatrix.SourceGen;

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
            .Select(static (text, ct) => DmgOps.FromJson(text.GetText(ct)?.ToString()))
            .Collect();

        IncrementalValuesProvider<MethodInfo> generatedAttributePipeline =
            context.SyntaxProvider.ForAttributeWithMetadataName(
                fullyQualifiedMetadataName: $"{Names.GeneratedNamespace}.{Names.GeneratedAttribute}",
                predicate: static (syntaxNode, _) => syntaxNode is BaseMethodDeclarationSyntax,
                transform: static (context, _) =>
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

        IncrementalValuesProvider<InstructionGenerationData> instructionPipeline = generatedAttributePipeline
            .Combine(opcodesPipeline)
            .Select((pair, _) => GetInstructionData(pair));

        context.RegisterSourceOutput(instructionPipeline, InstructionsHelper.GenerateAllInstructions);
    }

    private static InstructionGenerationData GetInstructionData(
        (MethodInfo MethodPath, ImmutableArray<DmgOps> Instructions) pair)
    {
        if (pair.Instructions.Length > 1)
        {
            throw new InvalidDataException(
                "Found more than one opcode json file. Only one input is supported");
        }

        return new InstructionGenerationData(pair.MethodPath, pair.Instructions.First());
    }
}
