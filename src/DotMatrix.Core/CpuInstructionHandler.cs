namespace DotMatrix.Core;

public class CpuInstructionHandler : IInstructionHandler<int>
{
    // private int Add(byte r8, int cycles = 4)
    // {
    //     // add two bytes => get a ushort
    //     ushort result = (ushort)(_cpuState.A + r8);
    //     _cpuState.SetZ(result);
    //     // leave as a ushort so we can detect overflow and cast to byte when assigning to accumulator
    //     _cpuState.SetC(result);
    //     _cpuState.A = (byte)(result & 0xFF);
    //     return cycles;
    // }

    public int Nop(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Load16(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Inc16(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Dec16(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int AddHl(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Inc(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Dec(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Rlca(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Rrca(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Rla(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Rra(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Daa(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Cpl(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Scf(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Ccf(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Jr(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int JrCond(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Stop(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Load8(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Halt(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int AddA(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int AdcA(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int SubA(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int SbcA(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int AndA(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int XorA(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int OrA(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int CpA(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int RetCond(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Ret(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Reti(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int JpCond(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Jp(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int JpHl(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int CallCond(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Call(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Rst(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int PopStack(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int PushStack(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Rlc(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Rrc(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Rl(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Rr(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Sla(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Sra(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Swap(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Srl(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Bit(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Set(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }

    public int Res(byte opcode, ref CpuState state)
    {
        throw new NotImplementedException();
    }
}
