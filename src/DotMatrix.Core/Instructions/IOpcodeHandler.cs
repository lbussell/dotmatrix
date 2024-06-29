namespace DotMatrix.Core.Instructions;

public interface IOpcodeHandler
{
    void HandleOpcode(ref CpuState state, IBus bus);
}
