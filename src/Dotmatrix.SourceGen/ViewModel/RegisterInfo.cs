using System.Text.RegularExpressions;

namespace DotMatrix.SourceGen.ViewModel;

public record RegisterInfo(Register Register, RegisterTarget Target, bool Indirect)
{
    private const string IndirectPattern = @"^\((.*)\)$";

    public static RegisterInfo FromString(string input)
    {
        Regex indirectRegex = new(IndirectPattern);
        Match indirectMatch = indirectRegex.Match(input);
        bool isIndirect = indirectMatch.Success;

        // Ignore the surrounding parens if we are indirect
        if (isIndirect)
        {
            input = indirectMatch.Groups[1].ToString();
        }

        return input switch
        {
            "A"  => new RegisterInfo(Register.AF, RegisterTarget.Hi,   isIndirect),
            "F"  => new RegisterInfo(Register.AF, RegisterTarget.Lo,   isIndirect),
            "AF" => new RegisterInfo(Register.AF, RegisterTarget.Both, isIndirect),

            "B"  => new RegisterInfo(Register.BC, RegisterTarget.Hi,   isIndirect),
            "C"  => new RegisterInfo(Register.BC, RegisterTarget.Lo,   isIndirect),
            "BC" => new RegisterInfo(Register.BC, RegisterTarget.Both, isIndirect),

            "D"  => new RegisterInfo(Register.DE, RegisterTarget.Hi,   isIndirect),
            "E"  => new RegisterInfo(Register.DE, RegisterTarget.Lo,   isIndirect),
            "DE" => new RegisterInfo(Register.DE, RegisterTarget.Both, isIndirect),

            "H"  => new RegisterInfo(Register.HL, RegisterTarget.Hi,   isIndirect),
            "L"  => new RegisterInfo(Register.HL, RegisterTarget.Lo,   isIndirect),
            "HL" => new RegisterInfo(Register.HL, RegisterTarget.Both, isIndirect),

            "SP" => new RegisterInfo(Register.SP, RegisterTarget.Both, isIndirect),
            "PC" => new RegisterInfo(Register.PC, RegisterTarget.Both, isIndirect),

            _ => throw new Exception("Got an unexpected value when parsing register"),
        };
    }
}
