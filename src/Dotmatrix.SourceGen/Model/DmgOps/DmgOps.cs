namespace DotMatrix.SourceGen.Model;

public record DmgOps(IEnumerable<Opcode> Unprefixed, IEnumerable<Opcode> CBPrefixed);
