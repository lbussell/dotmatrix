namespace Dotmatrix.SourceGen.Builders;

internal static class ClassGenerator
{
    public static string CreateAttribute(string @namespace, string name) =>
        new CsharpBuilder().WithNamespace(@namespace)
            .AppendLine($"[global::System.CodeDom.Compiler.GeneratedCodeAttribute(\"{nameof(Dotmatrix)}\", null)]")
            .OpenScope($"public class {name} : Attribute")
            .CloseScope()
            .Build();
}
