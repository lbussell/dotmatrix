namespace DotMatrix.SourceGen.ViewModel;

public record NoOp : Instruction
{
    public string GenerateSource()
    {
        return "";
    }
};
