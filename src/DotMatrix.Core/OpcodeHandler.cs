namespace DotMatrix.Core;

/// <summary>
/// Handles mapping of opcodes to instructions.
/// </summary>
public class OpcodeHandler<TResult>(IInstructionHandler<TResult> instructionHandler)
{
    private readonly IInstructionHandler<TResult> _instructionHandler = instructionHandler;

    public TResult HandleOpcode(byte opcode, ref CpuState state)
    {
        return GetBlock(opcode) switch
        {
            0 => StepBlock0(opcode, ref state),
            1 => StepBlock1(opcode, ref state),
            2 => StepBlock2(opcode, ref state),
            _ => StepBlock3(opcode, ref state),
        };
    }

    private static byte GetBlock(byte opcode) =>
        (byte)((opcode & 0b_1100_0000) >> 6);

    private TResult StepBlock0(byte opcode, ref CpuState state)
    {
        if (opcode == 0)
        {
            return _instructionHandler.Nop(opcode, ref state);
        }

        return (opcode & 0x0F) switch
        {
            0b_0001 => _instructionHandler.NotImplemented("ld r16, imm16"),
            0b_0010 => _instructionHandler.NotImplemented("ld [r16mem], a"),
            0b_1010 => _instructionHandler.NotImplemented("ld a, [r16mem]"),
            0b_1000 => _instructionHandler.NotImplemented("ld [imm16], sp"),

            0b_0011 => _instructionHandler.NotImplemented("ld [imm16], sp"),
            0b_1011 => _instructionHandler.NotImplemented("ld [imm16], sp"),
            0b_1001 => _instructionHandler.NotImplemented("ld [imm16], sp"),
            _ => _instructionHandler.NotImplemented(opcode, state),
        };
    }

    private TResult StepBlock1(byte opcode, ref CpuState state)
    {
        int cycles = 4;

        byte target = (byte)(opcode & 0b_0011_1000 >> 3);
        byte source = (byte)(opcode & 0b_0000_0111);

        if (target == 6)
        {
            cycles += 4;
        }

        if (source == 6)
        {
            cycles += 4;
        }

        if (target == 6 && source == 6)
        {
            return _instructionHandler.Halt(opcode, ref state);
        }

        return _instructionHandler.NotImplemented(opcode, state);

        // return _instructionHandler.Load8ToRegister(
        //     ref DecodeR8(target), DecodeR8(source), cycles);
    }

    private TResult StepBlock2(byte opcode, ref CpuState state)
    {
        return _instructionHandler.NotImplemented(opcode, state);

        // byte r8 = DecodeR8((byte)(opcode & 0b_00000111));
        // int instr = (opcode & 0b_00111000) >> 3;
        // return instr switch
        // {
        //     0b_000 => Add(r8),
        //     0b_001 => Adc(r8),
        //     0b_010 => Sub(r8),
        //     0b_011 => Sbc(r8),
        //     0b_100 => And(r8),
        //     0b_101 => Xor(r8),
        //     0b_110 => Or(r8),
        //     /* 0b_111 */ _ => Cp(r8),
        // };
    }

    private TResult StepBlock3(byte opcode, ref CpuState state)
    {
        return _instructionHandler.NotImplemented(opcode, state);
    }

    // private ref byte DecodeR8(byte b)
    // {
    //     switch (b)
    //     {
    //         case 0:
    //             return ref _cpuState.B;
    //         case 1:
    //             return ref _cpuState.C;
    //         case 2:
    //             return ref _cpuState.D;
    //         case 3:
    //             return ref _cpuState.E;
    //         case 4:
    //             return ref _cpuState.H;
    //         case 5:
    //             return ref _cpuState.L;
    //         case 6:
    //             return ref IndirectRef(_cpuState.HL);
    //         case 7:
    //         default:
    //             return ref _cpuState.A;
    //     }
    // }
    //
    // private ref ushort DecodeR16(byte b)
    // {
    //     switch (b)
    //     {
    //         case 0:
    //             return ref _cpuState.BC;
    //         case 1:
    //             return ref _cpuState.DE;
    //         case 2:
    //             return ref _cpuState.HL;
    //         case 3:
    //         default:
    //             return ref _cpuState.SP;
    //     }
    // }
    //
    // private ref ushort DecodeR16Stack(byte b)
    // {
    //     switch (b)
    //     {
    //         case 0:
    //             return ref _cpuState.BC;
    //         case 1:
    //             return ref _cpuState.DE;
    //         case 2:
    //             return ref _cpuState.HL;
    //         case 3:
    //         default:
    //             return ref _cpuState.AF;
    //     }
    // }
}