namespace DotMatrix.Core;

public class OpcodeHandler
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

    private static void Load8(ref CpuState state, IBus bus)
    {
        if ((state.Ir & 0b_1100_0000) >> 6 == 0b_01)
        {
            byte source = DecodeR8(ref state, (byte)(state.Ir & 0b00000111));
            ref byte dest = ref DecodeR8(ref state, (byte)(state.Ir & 0b00111000 >> 3));
            state.TCycles += Load8Internal(ref dest, source);
            return;
        }

        throw new NotImplementedException();
    }

    private static int Load8Internal(ref byte dest, byte source)
    {
        dest = source;
        return 4;
    }

    private static void Load16(ref CpuState state, IBus bus)
    {
        throw new NotImplementedException();
    }

    private static void NoOp(ref CpuState state, IBus bus)
    {
        state.TCycles += 4;
    }

    private static ref byte DecodeR8(ref CpuState state, byte b)
    {
        switch (b)
        {
            case 0:
                return ref state.B;
            case 1:
                return ref state.C;
            case 2:
                return ref state.D;
            case 3:
                return ref state.E;
            case 4:
                return ref state.H;
            case 5:
                return ref state.L;
            case 6:
                throw new NotImplementedException();
            // case 7:
            default:
                return ref state.A;
        }
    }
}
