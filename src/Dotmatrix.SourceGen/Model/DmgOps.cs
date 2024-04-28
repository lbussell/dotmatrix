namespace Dotmatrix.SourceGen.Model;

public record DmgOps(IEnumerable<Opcode> Unprefixed, IEnumerable<Opcode> CBPrefixed);
