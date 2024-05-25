using System.Text.RegularExpressions;
using DotMatrix.SourceGen.Builders;
using DotMatrix.SourceGen.Model.Instructions;

namespace DotMatrix.SourceGen.ViewModel;

public record Load(RegisterInfo Target, RegisterInfo Source, int Cycles)
    : Instruction
{
    public const string Pattern = @"^LD (.*)?,(.*)?$";

    public string GenerateSource()
    {
        return $"set {Target} <= read {Source}, elapsed {Cycles}";
    }

    public static Instruction FromOpcode(Opcode opcode)
    {
        Regex regex = new(Pattern);
        Match match = regex.Match(opcode.Name);
        if (!match.Success)
        {
            throw new Exception(
                $"Didn't get a regex match when we expected one. Input {opcode.Name} Pattern {Pattern}");
        }

        RegisterInfo target = RegisterInfo.FromString(match.Groups[1].ToString());
        RegisterInfo source = RegisterInfo.FromString(match.Groups[2].ToString());

        // Load instructions never branch.
        return new Load(target, source, opcode.TCyclesNoBranch);
    }
}
