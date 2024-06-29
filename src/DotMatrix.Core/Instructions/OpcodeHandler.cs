namespace DotMatrix.Core.Instructions;

public partial class OpcodeHandler
{
    private delegate void Instruction(ref CpuState state, IBus bus);

    private static readonly Instruction[] s_instructions =
    [
        NoOp,       Load16,     Load8,      Inc16,      Inc8,       Dec8,       Load8,      Rlca,
        Load16,     Add16,      Load8,      Dec16,      Inc8,       Dec8,       Load8,      Rrca,
        Stop,       Load16,     Load8,      Inc16,      Inc8,       Dec8,       Load8,      Rla,
        Jr,         Add16,      Load8,      Dec16,      Inc8,       Dec8,       Load8,      Rra,
        Jr,         Load16,     Load8,      Inc16,      Inc8,       Dec8,       Load8,      Daa,
        Jr,         Add16,      Load8,      Dec16,      Inc8,       Dec8,       Load8,      Cpl,
        Jr,         Load16,     Load8,      Inc16,      Inc8,       Dec8,       Load8,      Scf,
        Jr,         Add16,      Load8,      Dec16,      Inc8,       Dec8,       Load8,      Ccf,

        Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,
        Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,
        Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,
        Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,
        Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,
        Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,
        Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Halt,       Load8,
        Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,      Load8,

        Add,        Add,        Add,        Add,        Add,        Add,        Add,        Add,
        Adc,        Adc,        Adc,        Adc,        Adc,        Adc,        Adc,        Adc,
        Sub,        Sub,        Sub,        Sub,        Sub,        Sub,        Sub,        Sub,
        Sbc,        Sbc,        Sbc,        Sbc,        Sbc,        Sbc,        Sbc,        Sbc,
        And,        And,        And,        And,        And,        And,        And,        And,
        Xor,        Xor,        Xor,        Xor,        Xor,        Xor,        Xor,        Xor,
        Or,         Or,         Or,         Or,         Or,         Or,         Or,         Or,
        Cp,         Cp,         Cp,         Cp,         Cp,         Cp,         Cp,         Cp,

        Ret,        Pop,        Jp,         Jp,         Call,       Push,       Add,        Rst,
        Ret,        Ret,        Jp,         Cb,         Call,       Call,       Adc,        Rst,
        Ret,        Pop,        Jp,         Undef,      Call,       Push,       Sub,        Rst,
        Ret,        Reti,       Jp,         Undef,      Call,       Undef,      Sbc,        Rst,
        Load8,      Pop,        Load8,      Undef,      Undef,      Push,       And,        Rst,
        Add16,      Jp,         Load8,      Undef,      Undef,      Undef,      Xor,        Rst,
        Load8,      Pop,        Load8,      Di,         Undef,      Push,       Or,         Rst,
        Add16,      Reti,       Load8,      Di,         Undef,      Undef,      Cp,         Rst,
    ];

    private static void Load8(ref CpuState state, IBus bus) => Instructions.Load8.Load8Impl(ref state, bus);

    private static void Load16(ref CpuState state, IBus bus) => Instructions.Load16.Load16Impl(ref state, bus);

    public void HandleOpcode(ref CpuState state, IBus bus)
    {
        s_instructions[state.Ir](ref state, bus);
    }

    private static void Cb(ref CpuState state, IBus bus)
    {
        state.IrIsCb = true;
        throw new NotImplementedException();
    }

    private static void Di(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Reti(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Push(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Call(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Jp(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Pop(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Ret(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Cp(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Or(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Xor(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Ccf(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Scf(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Cpl(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Daa(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Halt(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Sbc(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void And(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Rra(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Rla(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Adc(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Sub(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Rlca(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Undef(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Rst(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Rrca(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Dec8(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Dec16(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Add16(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Stop(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Jr(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Add(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Inc8(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void Inc16(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void NoOp(ref CpuState state, IBus bus)
    {
        state.TCycles += 4;
    }
}
