using DotMatrix.SourceGen.Model.Instructions;

namespace DotMatrix.SourceGen.Model.Generator;

internal record InstructionGenerationData(MethodInfo MethodInfo, DmgOps Instructions);
