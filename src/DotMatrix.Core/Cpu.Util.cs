namespace DotMatrix.Core;

internal partial class Cpu
{
    private ref byte DecodeR8(byte b)
    {
        switch (b)
        {
            case 0:
                return ref _cpuState.B;
            case 1:
                return ref _cpuState.C;
            case 2:
                return ref _cpuState.D;
            case 3:
                return ref _cpuState.E;
            case 4:
                return ref _cpuState.H;
            case 5:
                return ref _cpuState.L;
            case 6:
                return ref IndirectRef(_cpuState.HL);
            case 7:
            default:
                return ref _cpuState.A;
        }
    }

    private ref ushort DecodeR16(byte b)
    {
        switch (b)
        {
            case 0:
                return ref _cpuState.BC;
            case 1:
                return ref _cpuState.DE;
            case 2:
                return ref _cpuState.HL;
            case 3:
            default:
                return ref _cpuState.SP;
        }
    }

    private ref ushort DecodeR16Stack(byte b)
    {
        switch (b)
        {
            case 0:
                return ref _cpuState.BC;
            case 1:
                return ref _cpuState.DE;
            case 2:
                return ref _cpuState.HL;
            case 3:
            default:
                return ref _cpuState.AF;
        }
    }

    private int NotImplemented(byte opcode) => NotImplemented(--_cpuState.PC, opcode);

    private static int NotImplemented(ushort address, byte opcode) =>
        throw new NotImplementedException(
            $"Encountered un-implemented opcode ${opcode:X2} at address ${address:X4}");

    private byte Indirect(ushort register) => _bus[register];
    private ref byte IndirectRef(ushort addr) => ref _bus.MMap(addr)[addr];

    private byte Immediate() => _bus[_cpuState.PC++];
    private ushort Immediate16() => (ushort)(_bus[_cpuState.PC++] | _bus[_cpuState.PC++] << 8);
}
