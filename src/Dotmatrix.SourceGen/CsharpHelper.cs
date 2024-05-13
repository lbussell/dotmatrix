using DotMatrix.SourceGen.Builders;

namespace DotMatrix.SourceGen;

internal static class CsharpHelper
{
    public static string CreateAttribute(string @namespace, string name) =>
        new CsharpBuilder().WithNamespace(@namespace)
            .AppendLine($"[global::System.CodeDom.Compiler.GeneratedCodeAttribute(\"{nameof(DotMatrix)}\", null)]")
            .OpenScope($"public class {name} : Attribute")
            .CloseScope()
            .Build();
}
