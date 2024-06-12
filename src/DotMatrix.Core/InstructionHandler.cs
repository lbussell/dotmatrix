namespace DotMatrix.Core;

public interface IInstructionHandler<out T>
{
    public virtual T NotImplemented(byte opcode, CpuState state) =>
        NotImplemented($"Opcode ${opcode:X2} not implemented");

    public virtual T NotImplemented(string message) =>
        throw new NotImplementedException(message);

    T Nop(byte opcode, ref CpuState state);

    T Load16(byte opcode, ref CpuState state);
    T Inc16(byte opcode, ref CpuState state);
    T Dec16(byte opcode, ref CpuState state);
    T AddHl(byte opcode, ref CpuState state);
    T Inc(byte opcode, ref CpuState state);
    T Dec(byte opcode, ref CpuState state);
    T Rlca(byte opcode, ref CpuState state);
    T Rrca(byte opcode, ref CpuState state);
    T Rla(byte opcode, ref CpuState state);
    T Rra(byte opcode, ref CpuState state);
    T Daa(byte opcode, ref CpuState state);
    T Cpl(byte opcode, ref CpuState state);
    T Scf(byte opcode, ref CpuState state);
    T Ccf(byte opcode, ref CpuState state);
    T Jr(byte opcode, ref CpuState state);
    T JrCond(byte opcode, ref CpuState state);
    T Stop(byte opcode, ref CpuState state);

    // Block 1
    T Load8(byte opcode, ref CpuState state);
    T Halt(byte opcode, ref CpuState state);

    // Block 2
    T AddA(byte opcode, ref CpuState state);
    T AdcA(byte opcode, ref CpuState state);
    T SubA(byte opcode, ref CpuState state);
    T SbcA(byte opcode, ref CpuState state);
    T AndA(byte opcode, ref CpuState state);
    T XorA(byte opcode, ref CpuState state);
    T OrA(byte opcode, ref CpuState state);
    T CpA(byte opcode, ref CpuState state);

    // Block 3
    T RetCond(byte opcode, ref CpuState state);
    T Ret(byte opcode, ref CpuState state);
    T Reti(byte opcode, ref CpuState state);
    T JpCond(byte opcode, ref CpuState state);
    T Jp(byte opcode, ref CpuState state);
    T JpHl(byte opcode, ref CpuState state);
    T CallCond(byte opcode, ref CpuState state);
    T Call(byte opcode, ref CpuState state);
    T Rst(byte opcode, ref CpuState state);
    T PopStack(byte opcode, ref CpuState state);
    T PushStack(byte opcode, ref CpuState state);
    /* Some more that's not done yet... */

    // Prefix
    T Rlc(byte opcode, ref CpuState state);
    T Rrc(byte opcode, ref CpuState state);
    T Rl(byte opcode, ref CpuState state);
    T Rr(byte opcode, ref CpuState state);
    T Sla(byte opcode, ref CpuState state);
    T Sra(byte opcode, ref CpuState state);
    T Swap(byte opcode, ref CpuState state);
    T Srl(byte opcode, ref CpuState state);

    T Bit(byte opcode, ref CpuState state);
    T Set(byte opcode, ref CpuState state);
    T Res(byte opcode, ref CpuState state);
}
