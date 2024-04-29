namespace Dotmatrix.SourceGen.Builders;

using System.Text;

public sealed class CsharpBuilder(int Spaces = 4)
{
    private readonly StringBuilder _builder = new();

    private int _indentationLevel;

    private string Indentation => new(' ', _indentationLevel * Spaces);

    public CsharpBuilder WithNamespace(string name)
    {
        _builder.Insert(0, $"namespace {name};\n");
        return this;
    }

    public CsharpBuilder OpenScope(string line = "")
    {
        AppendLine(line);
        AppendLine("{");
        IncreaseIndent();
        return this;
    }

    public CsharpBuilder CloseScope()
    {
        DecreaseIndent();
        AppendLine("}");
        return this;
    }

    public CsharpBuilder AppendLine(string line = "")
    {
        string indentation = !string.IsNullOrEmpty(line) ? Indentation : "";
        _builder.AppendLine(indentation + line);
        return this;
    }

    public string Build()
    {
        while (_indentationLevel != 0)
        {
            CloseScope();
        }

        return _builder.ToString();
    }

    private void IncreaseIndent()
    {
        _indentationLevel += 1;
    }

    private void DecreaseIndent()
    {
        _indentationLevel -= 1;
    }
}
