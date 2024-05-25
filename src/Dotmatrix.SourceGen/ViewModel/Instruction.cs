using System.Text.RegularExpressions;
using DotMatrix.SourceGen.Model.Instructions;

namespace DotMatrix.SourceGen.ViewModel;

public interface Instruction
{
    string GenerateSource();
}

public static class InstructionsConverter
{
    public static Instruction FromOpcode(Opcode opcode)
    {
        if (Regex.IsMatch(opcode.Name, Load.Pattern))
        {
            return Load.FromOpcode(opcode);
        }

        return new NoOp();
    }
}
