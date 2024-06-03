namespace DotMatrix.Core;

internal partial class Cpu
{
    // returns: number of cycles elapsed
    private int Step()
    {
        byte op = _bus[_cpuState.PC++];
        Console.WriteLine($"Ex ${op:X2}");

        return op switch
        {
            0xCB => CBPrefix(ReadInc8()),

            0x00 => NotImplemented(--_cpuState.PC, op),
            0x01 => NotImplemented(--_cpuState.PC, op),
            0x02 => Load8ToMemory(_cpuState.BC, _cpuState.A),
            0x03 => NotImplemented(--_cpuState.PC, op),
            0x04 => NotImplemented(--_cpuState.PC, op),
            0x05 => NotImplemented(--_cpuState.PC, op),
            0x06 => Load8ToRegister(ref _cpuState.B, Immediate()),
            0x07 => NotImplemented(--_cpuState.PC, op),
            0x08 => NotImplemented(--_cpuState.PC, op),
            0x09 => NotImplemented(--_cpuState.PC, op),
            0x0A => Load8ToRegister(ref _cpuState.A, Indirect(_cpuState.BC)),
            0x0B => NotImplemented(--_cpuState.PC, op),
            0x0C => NotImplemented(--_cpuState.PC, op),
            0x0D => NotImplemented(--_cpuState.PC, op),
            0x0E => Load8ToRegister(ref _cpuState.C, Immediate()),
            0x0F => NotImplemented(--_cpuState.PC, op),

            0x10 => NotImplemented(--_cpuState.PC, op),
            0x11 => NotImplemented(--_cpuState.PC, op),
            0x12 => Load8ToMemory(_cpuState.DE, _cpuState.A),
            0x13 => NotImplemented(--_cpuState.PC, op),
            0x14 => NotImplemented(--_cpuState.PC, op),
            0x15 => NotImplemented(--_cpuState.PC, op),
            0x16 => Load8ToRegister(ref _cpuState.D, Immediate()),
            0x17 => NotImplemented(--_cpuState.PC, op),
            0x18 => NotImplemented(--_cpuState.PC, op),
            0x19 => NotImplemented(--_cpuState.PC, op),
            0x1A => Load8ToRegister(ref _cpuState.A, Indirect(_cpuState.DE)),
            0x1B => NotImplemented(--_cpuState.PC, op),
            0x1C => NotImplemented(--_cpuState.PC, op),
            0x1D => NotImplemented(--_cpuState.PC, op),
            0x1E => Load8ToRegister(ref _cpuState.E, Immediate()),
            0x1F => NotImplemented(--_cpuState.PC, op),

            0x20 => NotImplemented(--_cpuState.PC, op),
            0x21 => NotImplemented(--_cpuState.PC, op),
            0x22 => Load8ToMemory(_cpuState.HL++, _cpuState.A),
            0x23 => NotImplemented(--_cpuState.PC, op),
            0x24 => NotImplemented(--_cpuState.PC, op),
            0x25 => NotImplemented(--_cpuState.PC, op),
            0x26 => Load8ToRegister(ref _cpuState.H, Immediate()),
            0x27 => NotImplemented(--_cpuState.PC, op),
            0x28 => NotImplemented(--_cpuState.PC, op),
            0x29 => NotImplemented(--_cpuState.PC, op),
            0x2A => Load8ToRegister(ref _cpuState.A, Indirect(_cpuState.HL++)),
            0x2B => NotImplemented(--_cpuState.PC, op),
            0x2C => NotImplemented(--_cpuState.PC, op),
            0x2D => NotImplemented(--_cpuState.PC, op),
            0x2E => Load8ToRegister(ref _cpuState.L, Immediate()),
            0x2F => NotImplemented(--_cpuState.PC, op),

            0x30 => NotImplemented(--_cpuState.PC, op),
            0x31 => NotImplemented(--_cpuState.PC, op),
            0x32 => Load8ToMemory(_cpuState.HL--, _cpuState.A),
            0x33 => NotImplemented(--_cpuState.PC, op),
            0x34 => NotImplemented(--_cpuState.PC, op),
            0x35 => NotImplemented(--_cpuState.PC, op),
            0x36 => Load8ToMemory(Indirect(_cpuState.HL), Immediate(), 12),
            0x37 => NotImplemented(--_cpuState.PC, op),
            0x38 => NotImplemented(--_cpuState.PC, op),
            0x39 => NotImplemented(--_cpuState.PC, op),
            0x3A => Load8ToRegister(ref _cpuState.A, Indirect(_cpuState.HL--)),
            0x3B => NotImplemented(--_cpuState.PC, op),
            0x3C => NotImplemented(--_cpuState.PC, op),
            0x3D => NotImplemented(--_cpuState.PC, op),
            0x3E => Load8ToRegister(ref _cpuState.A, Immediate()),
            0x3F => NotImplemented(--_cpuState.PC, op),

            0x40 => Load8ToRegister(ref _cpuState.B, _cpuState.B),
            0x41 => Load8ToRegister(ref _cpuState.B, _cpuState.C),
            0x42 => Load8ToRegister(ref _cpuState.B, _cpuState.D),
            0x43 => Load8ToRegister(ref _cpuState.B, _cpuState.E),
            0x44 => Load8ToRegister(ref _cpuState.B, _cpuState.H),
            0x45 => Load8ToRegister(ref _cpuState.B, _cpuState.L),
            0x46 => Load8ToRegister(ref _cpuState.B, Indirect(_cpuState.HL), 8),
            0x47 => Load8ToRegister(ref _cpuState.B, _cpuState.A),
            0x48 => Load8ToRegister(ref _cpuState.C, _cpuState.B),
            0x49 => Load8ToRegister(ref _cpuState.C, _cpuState.C),
            0x4A => Load8ToRegister(ref _cpuState.C, _cpuState.D),
            0x4B => Load8ToRegister(ref _cpuState.C, _cpuState.E),
            0x4C => Load8ToRegister(ref _cpuState.C, _cpuState.H),
            0x4D => Load8ToRegister(ref _cpuState.C, _cpuState.L),
            0x4E => Load8ToRegister(ref _cpuState.C, Indirect(_cpuState.HL), 8),
            0x4F => Load8ToRegister(ref _cpuState.C, _cpuState.A),

            0x50 => Load8ToRegister(ref _cpuState.D, _cpuState.B),
            0x51 => Load8ToRegister(ref _cpuState.D, _cpuState.C),
            0x52 => Load8ToRegister(ref _cpuState.D, _cpuState.D),
            0x53 => Load8ToRegister(ref _cpuState.D, _cpuState.E),
            0x54 => Load8ToRegister(ref _cpuState.D, _cpuState.H),
            0x55 => Load8ToRegister(ref _cpuState.D, _cpuState.L),
            0x56 => Load8ToRegister(ref _cpuState.D, Indirect(_cpuState.HL), 8),
            0x57 => Load8ToRegister(ref _cpuState.D, _cpuState.A),
            0x58 => Load8ToRegister(ref _cpuState.E, _cpuState.B),
            0x59 => Load8ToRegister(ref _cpuState.E, _cpuState.C),
            0x5A => Load8ToRegister(ref _cpuState.E, _cpuState.D),
            0x5B => Load8ToRegister(ref _cpuState.E, _cpuState.E),
            0x5C => Load8ToRegister(ref _cpuState.E, _cpuState.H),
            0x5D => Load8ToRegister(ref _cpuState.E, _cpuState.L),
            0x5E => Load8ToRegister(ref _cpuState.E, Indirect(_cpuState.HL), 8),
            0x5F => Load8ToRegister(ref _cpuState.E, _cpuState.A),

            0x60 => Load8ToRegister(ref _cpuState.H, _cpuState.B),
            0x61 => Load8ToRegister(ref _cpuState.H, _cpuState.C),
            0x62 => Load8ToRegister(ref _cpuState.H, _cpuState.D),
            0x63 => Load8ToRegister(ref _cpuState.H, _cpuState.E),
            0x64 => Load8ToRegister(ref _cpuState.H, _cpuState.H),
            0x65 => Load8ToRegister(ref _cpuState.H, _cpuState.L),
            0x66 => Load8ToRegister(ref _cpuState.H, Indirect(_cpuState.HL), 8),
            0x67 => Load8ToRegister(ref _cpuState.H, _cpuState.A),
            0x68 => Load8ToRegister(ref _cpuState.L, _cpuState.B),
            0x69 => Load8ToRegister(ref _cpuState.L, _cpuState.C),
            0x6A => Load8ToRegister(ref _cpuState.L, _cpuState.D),
            0x6B => Load8ToRegister(ref _cpuState.L, _cpuState.E),
            0x6C => Load8ToRegister(ref _cpuState.L, _cpuState.H),
            0x6D => Load8ToRegister(ref _cpuState.L, _cpuState.L),
            0x6E => Load8ToRegister(ref _cpuState.L, Indirect(_cpuState.HL), 8),
            0x6F => Load8ToRegister(ref _cpuState.L, _cpuState.A),

            0x70 => Load8ToMemory(_cpuState.HL, _cpuState.B),
            0x71 => Load8ToMemory(_cpuState.HL, _cpuState.C),
            0x72 => Load8ToMemory(_cpuState.HL, _cpuState.D),
            0x73 => Load8ToMemory(_cpuState.HL, _cpuState.E),
            0x74 => Load8ToMemory(_cpuState.HL, _cpuState.H),
            0x75 => Load8ToMemory(_cpuState.HL, _cpuState.L),
            0x76 => Halt(),
            0x77 => Load8ToMemory(_cpuState.HL, _cpuState.A),
            0x78 => Load8ToRegister(ref _cpuState.A, _cpuState.B),
            0x79 => Load8ToRegister(ref _cpuState.A, _cpuState.C),
            0x7A => Load8ToRegister(ref _cpuState.A, _cpuState.D),
            0x7B => Load8ToRegister(ref _cpuState.A, _cpuState.E),
            0x7C => Load8ToRegister(ref _cpuState.A, _cpuState.H),
            0x7D => Load8ToRegister(ref _cpuState.A, _cpuState.L),
            0x7E => Load8ToRegister(ref _cpuState.A, Indirect(_cpuState.HL)),
            0x7F => Load8ToRegister(ref _cpuState.A, _cpuState.A),

            0x80 => NotImplemented(--_cpuState.PC, op),
            0x81 => NotImplemented(--_cpuState.PC, op),
            0x82 => NotImplemented(--_cpuState.PC, op),
            0x83 => NotImplemented(--_cpuState.PC, op),
            0x84 => NotImplemented(--_cpuState.PC, op),
            0x85 => NotImplemented(--_cpuState.PC, op),
            0x86 => NotImplemented(--_cpuState.PC, op),
            0x87 => NotImplemented(--_cpuState.PC, op),
            0x88 => NotImplemented(--_cpuState.PC, op),
            0x89 => NotImplemented(--_cpuState.PC, op),
            0x8A => NotImplemented(--_cpuState.PC, op),
            0x8B => NotImplemented(--_cpuState.PC, op),
            0x8C => NotImplemented(--_cpuState.PC, op),
            0x8D => NotImplemented(--_cpuState.PC, op),
            0x8E => NotImplemented(--_cpuState.PC, op),
            0x8F => NotImplemented(--_cpuState.PC, op),

            0x90 => NotImplemented(--_cpuState.PC, op),
            0x91 => NotImplemented(--_cpuState.PC, op),
            0x92 => NotImplemented(--_cpuState.PC, op),
            0x93 => NotImplemented(--_cpuState.PC, op),
            0x94 => NotImplemented(--_cpuState.PC, op),
            0x95 => NotImplemented(--_cpuState.PC, op),
            0x96 => NotImplemented(--_cpuState.PC, op),
            0x97 => NotImplemented(--_cpuState.PC, op),
            0x98 => NotImplemented(--_cpuState.PC, op),
            0x99 => NotImplemented(--_cpuState.PC, op),
            0x9A => NotImplemented(--_cpuState.PC, op),
            0x9B => NotImplemented(--_cpuState.PC, op),
            0x9C => NotImplemented(--_cpuState.PC, op),
            0x9D => NotImplemented(--_cpuState.PC, op),
            0x9E => NotImplemented(--_cpuState.PC, op),
            0x9F => NotImplemented(--_cpuState.PC, op),

            0xA0 => NotImplemented(--_cpuState.PC, op),
            0xA1 => NotImplemented(--_cpuState.PC, op),
            0xA2 => NotImplemented(--_cpuState.PC, op),
            0xA3 => NotImplemented(--_cpuState.PC, op),
            0xA4 => NotImplemented(--_cpuState.PC, op),
            0xA5 => NotImplemented(--_cpuState.PC, op),
            0xA6 => NotImplemented(--_cpuState.PC, op),
            0xA7 => NotImplemented(--_cpuState.PC, op),
            0xA8 => NotImplemented(--_cpuState.PC, op),
            0xA9 => NotImplemented(--_cpuState.PC, op),
            0xAA => NotImplemented(--_cpuState.PC, op),
            0xAB => NotImplemented(--_cpuState.PC, op),
            0xAC => NotImplemented(--_cpuState.PC, op),
            0xAD => NotImplemented(--_cpuState.PC, op),
            0xAE => NotImplemented(--_cpuState.PC, op),
            0xAF => NotImplemented(--_cpuState.PC, op),

            0xB0 => NotImplemented(--_cpuState.PC, op),
            0xB1 => NotImplemented(--_cpuState.PC, op),
            0xB2 => NotImplemented(--_cpuState.PC, op),
            0xB3 => NotImplemented(--_cpuState.PC, op),
            0xB4 => NotImplemented(--_cpuState.PC, op),
            0xB5 => NotImplemented(--_cpuState.PC, op),
            0xB6 => NotImplemented(--_cpuState.PC, op),
            0xB7 => NotImplemented(--_cpuState.PC, op),
            0xB8 => NotImplemented(--_cpuState.PC, op),
            0xB9 => NotImplemented(--_cpuState.PC, op),
            0xBA => NotImplemented(--_cpuState.PC, op),
            0xBB => NotImplemented(--_cpuState.PC, op),
            0xBC => NotImplemented(--_cpuState.PC, op),
            0xBD => NotImplemented(--_cpuState.PC, op),
            0xBE => NotImplemented(--_cpuState.PC, op),
            0xBF => NotImplemented(--_cpuState.PC, op),

            0xC0 => NotImplemented(--_cpuState.PC, op),
            0xC1 => NotImplemented(--_cpuState.PC, op),
            0xC2 => NotImplemented(--_cpuState.PC, op),
            0xC3 => NotImplemented(--_cpuState.PC, op),
            0xC4 => NotImplemented(--_cpuState.PC, op),
            0xC5 => NotImplemented(--_cpuState.PC, op),
            0xC6 => NotImplemented(--_cpuState.PC, op),
            0xC7 => NotImplemented(--_cpuState.PC, op),
            0xC8 => NotImplemented(--_cpuState.PC, op),
            0xC9 => NotImplemented(--_cpuState.PC, op),
            0xCA => NotImplemented(--_cpuState.PC, op),
            // 0xCB => NotImplemented(--_cpuState.PC, op),
            0xCC => NotImplemented(--_cpuState.PC, op),
            0xCD => NotImplemented(--_cpuState.PC, op),
            0xCE => NotImplemented(--_cpuState.PC, op),
            0xCF => NotImplemented(--_cpuState.PC, op),

            0xD0 => NotImplemented(--_cpuState.PC, op),
            0xD1 => NotImplemented(--_cpuState.PC, op),
            0xD2 => NotImplemented(--_cpuState.PC, op),
            0xD3 => NotImplemented(--_cpuState.PC, op),
            0xD4 => NotImplemented(--_cpuState.PC, op),
            0xD5 => NotImplemented(--_cpuState.PC, op),
            0xD6 => NotImplemented(--_cpuState.PC, op),
            0xD7 => NotImplemented(--_cpuState.PC, op),
            0xD8 => NotImplemented(--_cpuState.PC, op),
            0xD9 => NotImplemented(--_cpuState.PC, op),
            0xDA => NotImplemented(--_cpuState.PC, op),
            0xDB => NotImplemented(--_cpuState.PC, op),
            0xDC => NotImplemented(--_cpuState.PC, op),
            0xDD => NotImplemented(--_cpuState.PC, op),
            0xDE => NotImplemented(--_cpuState.PC, op),
            0xDF => NotImplemented(--_cpuState.PC, op),

            0xE0 => Load8ToMemory((ushort)(0xFF00 + Immediate()), _cpuState.A, 12),
            0xE1 => NotImplemented(--_cpuState.PC, op),
            0xE2 => Load8ToMemory((ushort)(0xFF00 + _cpuState.C), _cpuState.A),
            0xE3 => NotImplemented(--_cpuState.PC, op),
            0xE4 => NotImplemented(--_cpuState.PC, op),
            0xE5 => NotImplemented(--_cpuState.PC, op),
            0xE6 => NotImplemented(--_cpuState.PC, op),
            0xE7 => NotImplemented(--_cpuState.PC, op),
            0xE8 => NotImplemented(--_cpuState.PC, op),
            0xE9 => NotImplemented(--_cpuState.PC, op),
            0xEA => NotImplemented(--_cpuState.PC, op),
            0xEB => NotImplemented(--_cpuState.PC, op),
            0xEC => NotImplemented(--_cpuState.PC, op),
            0xED => NotImplemented(--_cpuState.PC, op),
            0xEE => NotImplemented(--_cpuState.PC, op),
            0xEF => NotImplemented(--_cpuState.PC, op),

            0xF0 => Load8ToRegister(ref _cpuState.A, Indirect((ushort)(0xFF00 + Immediate())), 12),
            0xF1 => NotImplemented(--_cpuState.PC, op),
            0xF2 => Load8ToRegister(ref _cpuState.A, Indirect((ushort)(0xFF00 + _cpuState.C)), 12),
            0xF3 => NotImplemented(--_cpuState.PC, op),
            0xF4 => NotImplemented(--_cpuState.PC, op),
            0xF5 => NotImplemented(--_cpuState.PC, op),
            0xF6 => NotImplemented(--_cpuState.PC, op),
            0xF7 => NotImplemented(--_cpuState.PC, op),
            0xF8 => NotImplemented(--_cpuState.PC, op),
            0xF9 => NotImplemented(--_cpuState.PC, op),
            0xFA => NotImplemented(--_cpuState.PC, op),
            0xFB => NotImplemented(--_cpuState.PC, op),
            0xFC => NotImplemented(--_cpuState.PC, op),
            0xFD => NotImplemented(--_cpuState.PC, op),
            0xFE => NotImplemented(--_cpuState.PC, op),
            0xFF => NotImplemented(--_cpuState.PC, op),
        };
    }

    private int CBPrefix(byte op)
    {
        return op switch
        {
            0x00 => NotImplemented(--_cpuState.PC, op),
            0x01 => NotImplemented(--_cpuState.PC, op),
            0x02 => NotImplemented(--_cpuState.PC, op),
            0x03 => NotImplemented(--_cpuState.PC, op),
            0x04 => NotImplemented(--_cpuState.PC, op),
            0x05 => NotImplemented(--_cpuState.PC, op),
            0x06 => NotImplemented(--_cpuState.PC, op),
            0x07 => NotImplemented(--_cpuState.PC, op),
            0x08 => NotImplemented(--_cpuState.PC, op),
            0x09 => NotImplemented(--_cpuState.PC, op),
            0x0A => NotImplemented(--_cpuState.PC, op),
            0x0B => NotImplemented(--_cpuState.PC, op),
            0x0C => NotImplemented(--_cpuState.PC, op),
            0x0D => NotImplemented(--_cpuState.PC, op),
            0x0E => NotImplemented(--_cpuState.PC, op),
            0x0F => NotImplemented(--_cpuState.PC, op),

            0x10 => NotImplemented(--_cpuState.PC, op),
            0x11 => NotImplemented(--_cpuState.PC, op),
            0x12 => NotImplemented(--_cpuState.PC, op),
            0x13 => NotImplemented(--_cpuState.PC, op),
            0x14 => NotImplemented(--_cpuState.PC, op),
            0x15 => NotImplemented(--_cpuState.PC, op),
            0x16 => NotImplemented(--_cpuState.PC, op),
            0x17 => NotImplemented(--_cpuState.PC, op),
            0x18 => NotImplemented(--_cpuState.PC, op),
            0x19 => NotImplemented(--_cpuState.PC, op),
            0x1A => NotImplemented(--_cpuState.PC, op),
            0x1B => NotImplemented(--_cpuState.PC, op),
            0x1C => NotImplemented(--_cpuState.PC, op),
            0x1D => NotImplemented(--_cpuState.PC, op),
            0x1E => NotImplemented(--_cpuState.PC, op),
            0x1F => NotImplemented(--_cpuState.PC, op),

            0x20 => NotImplemented(--_cpuState.PC, op),
            0x21 => NotImplemented(--_cpuState.PC, op),
            0x22 => NotImplemented(--_cpuState.PC, op),
            0x23 => NotImplemented(--_cpuState.PC, op),
            0x24 => NotImplemented(--_cpuState.PC, op),
            0x25 => NotImplemented(--_cpuState.PC, op),
            0x26 => NotImplemented(--_cpuState.PC, op),
            0x27 => NotImplemented(--_cpuState.PC, op),
            0x28 => NotImplemented(--_cpuState.PC, op),
            0x29 => NotImplemented(--_cpuState.PC, op),
            0x2A => NotImplemented(--_cpuState.PC, op),
            0x2B => NotImplemented(--_cpuState.PC, op),
            0x2C => NotImplemented(--_cpuState.PC, op),
            0x2D => NotImplemented(--_cpuState.PC, op),
            0x2E => NotImplemented(--_cpuState.PC, op),
            0x2F => NotImplemented(--_cpuState.PC, op),

            0x30 => NotImplemented(--_cpuState.PC, op),
            0x31 => NotImplemented(--_cpuState.PC, op),
            0x32 => NotImplemented(--_cpuState.PC, op),
            0x33 => NotImplemented(--_cpuState.PC, op),
            0x34 => NotImplemented(--_cpuState.PC, op),
            0x35 => NotImplemented(--_cpuState.PC, op),
            0x36 => NotImplemented(--_cpuState.PC, op),
            0x37 => NotImplemented(--_cpuState.PC, op),
            0x38 => NotImplemented(--_cpuState.PC, op),
            0x39 => NotImplemented(--_cpuState.PC, op),
            0x3A => NotImplemented(--_cpuState.PC, op),
            0x3B => NotImplemented(--_cpuState.PC, op),
            0x3C => NotImplemented(--_cpuState.PC, op),
            0x3D => NotImplemented(--_cpuState.PC, op),
            0x3E => NotImplemented(--_cpuState.PC, op),
            0x3F => NotImplemented(--_cpuState.PC, op),

            0x40 => NotImplemented(--_cpuState.PC, op),
            0x41 => NotImplemented(--_cpuState.PC, op),
            0x42 => NotImplemented(--_cpuState.PC, op),
            0x43 => NotImplemented(--_cpuState.PC, op),
            0x44 => NotImplemented(--_cpuState.PC, op),
            0x45 => NotImplemented(--_cpuState.PC, op),
            0x46 => NotImplemented(--_cpuState.PC, op),
            0x47 => NotImplemented(--_cpuState.PC, op),
            0x48 => NotImplemented(--_cpuState.PC, op),
            0x49 => NotImplemented(--_cpuState.PC, op),
            0x4A => NotImplemented(--_cpuState.PC, op),
            0x4B => NotImplemented(--_cpuState.PC, op),
            0x4C => NotImplemented(--_cpuState.PC, op),
            0x4D => NotImplemented(--_cpuState.PC, op),
            0x4E => NotImplemented(--_cpuState.PC, op),
            0x4F => NotImplemented(--_cpuState.PC, op),

            0x50 => NotImplemented(--_cpuState.PC, op),
            0x51 => NotImplemented(--_cpuState.PC, op),
            0x52 => NotImplemented(--_cpuState.PC, op),
            0x53 => NotImplemented(--_cpuState.PC, op),
            0x54 => NotImplemented(--_cpuState.PC, op),
            0x55 => NotImplemented(--_cpuState.PC, op),
            0x56 => NotImplemented(--_cpuState.PC, op),
            0x57 => NotImplemented(--_cpuState.PC, op),
            0x58 => NotImplemented(--_cpuState.PC, op),
            0x59 => NotImplemented(--_cpuState.PC, op),
            0x5A => NotImplemented(--_cpuState.PC, op),
            0x5B => NotImplemented(--_cpuState.PC, op),
            0x5C => NotImplemented(--_cpuState.PC, op),
            0x5D => NotImplemented(--_cpuState.PC, op),
            0x5E => NotImplemented(--_cpuState.PC, op),
            0x5F => NotImplemented(--_cpuState.PC, op),

            0x60 => NotImplemented(--_cpuState.PC, op),
            0x61 => NotImplemented(--_cpuState.PC, op),
            0x62 => NotImplemented(--_cpuState.PC, op),
            0x63 => NotImplemented(--_cpuState.PC, op),
            0x64 => NotImplemented(--_cpuState.PC, op),
            0x65 => NotImplemented(--_cpuState.PC, op),
            0x66 => NotImplemented(--_cpuState.PC, op),
            0x67 => NotImplemented(--_cpuState.PC, op),
            0x68 => NotImplemented(--_cpuState.PC, op),
            0x69 => NotImplemented(--_cpuState.PC, op),
            0x6A => NotImplemented(--_cpuState.PC, op),
            0x6B => NotImplemented(--_cpuState.PC, op),
            0x6C => NotImplemented(--_cpuState.PC, op),
            0x6D => NotImplemented(--_cpuState.PC, op),
            0x6E => NotImplemented(--_cpuState.PC, op),
            0x6F => NotImplemented(--_cpuState.PC, op),

            0x70 => NotImplemented(--_cpuState.PC, op),
            0x71 => NotImplemented(--_cpuState.PC, op),
            0x72 => NotImplemented(--_cpuState.PC, op),
            0x73 => NotImplemented(--_cpuState.PC, op),
            0x74 => NotImplemented(--_cpuState.PC, op),
            0x75 => NotImplemented(--_cpuState.PC, op),
            0x76 => NotImplemented(--_cpuState.PC, op),
            0x77 => NotImplemented(--_cpuState.PC, op),
            0x78 => NotImplemented(--_cpuState.PC, op),
            0x79 => NotImplemented(--_cpuState.PC, op),
            0x7A => NotImplemented(--_cpuState.PC, op),
            0x7B => NotImplemented(--_cpuState.PC, op),
            0x7C => NotImplemented(--_cpuState.PC, op),
            0x7D => NotImplemented(--_cpuState.PC, op),
            0x7E => NotImplemented(--_cpuState.PC, op),
            0x7F => NotImplemented(--_cpuState.PC, op),

            0x80 => NotImplemented(--_cpuState.PC, op),
            0x81 => NotImplemented(--_cpuState.PC, op),
            0x82 => NotImplemented(--_cpuState.PC, op),
            0x83 => NotImplemented(--_cpuState.PC, op),
            0x84 => NotImplemented(--_cpuState.PC, op),
            0x85 => NotImplemented(--_cpuState.PC, op),
            0x86 => NotImplemented(--_cpuState.PC, op),
            0x87 => NotImplemented(--_cpuState.PC, op),
            0x88 => NotImplemented(--_cpuState.PC, op),
            0x89 => NotImplemented(--_cpuState.PC, op),
            0x8A => NotImplemented(--_cpuState.PC, op),
            0x8B => NotImplemented(--_cpuState.PC, op),
            0x8C => NotImplemented(--_cpuState.PC, op),
            0x8D => NotImplemented(--_cpuState.PC, op),
            0x8E => NotImplemented(--_cpuState.PC, op),
            0x8F => NotImplemented(--_cpuState.PC, op),

            0x90 => NotImplemented(--_cpuState.PC, op),
            0x91 => NotImplemented(--_cpuState.PC, op),
            0x92 => NotImplemented(--_cpuState.PC, op),
            0x93 => NotImplemented(--_cpuState.PC, op),
            0x94 => NotImplemented(--_cpuState.PC, op),
            0x95 => NotImplemented(--_cpuState.PC, op),
            0x96 => NotImplemented(--_cpuState.PC, op),
            0x97 => NotImplemented(--_cpuState.PC, op),
            0x98 => NotImplemented(--_cpuState.PC, op),
            0x99 => NotImplemented(--_cpuState.PC, op),
            0x9A => NotImplemented(--_cpuState.PC, op),
            0x9B => NotImplemented(--_cpuState.PC, op),
            0x9C => NotImplemented(--_cpuState.PC, op),
            0x9D => NotImplemented(--_cpuState.PC, op),
            0x9E => NotImplemented(--_cpuState.PC, op),
            0x9F => NotImplemented(--_cpuState.PC, op),

            0xA0 => NotImplemented(--_cpuState.PC, op),
            0xA1 => NotImplemented(--_cpuState.PC, op),
            0xA2 => NotImplemented(--_cpuState.PC, op),
            0xA3 => NotImplemented(--_cpuState.PC, op),
            0xA4 => NotImplemented(--_cpuState.PC, op),
            0xA5 => NotImplemented(--_cpuState.PC, op),
            0xA6 => NotImplemented(--_cpuState.PC, op),
            0xA7 => NotImplemented(--_cpuState.PC, op),
            0xA8 => NotImplemented(--_cpuState.PC, op),
            0xA9 => NotImplemented(--_cpuState.PC, op),
            0xAA => NotImplemented(--_cpuState.PC, op),
            0xAB => NotImplemented(--_cpuState.PC, op),
            0xAC => NotImplemented(--_cpuState.PC, op),
            0xAD => NotImplemented(--_cpuState.PC, op),
            0xAE => NotImplemented(--_cpuState.PC, op),
            0xAF => NotImplemented(--_cpuState.PC, op),

            0xB0 => NotImplemented(--_cpuState.PC, op),
            0xB1 => NotImplemented(--_cpuState.PC, op),
            0xB2 => NotImplemented(--_cpuState.PC, op),
            0xB3 => NotImplemented(--_cpuState.PC, op),
            0xB4 => NotImplemented(--_cpuState.PC, op),
            0xB5 => NotImplemented(--_cpuState.PC, op),
            0xB6 => NotImplemented(--_cpuState.PC, op),
            0xB7 => NotImplemented(--_cpuState.PC, op),
            0xB8 => NotImplemented(--_cpuState.PC, op),
            0xB9 => NotImplemented(--_cpuState.PC, op),
            0xBA => NotImplemented(--_cpuState.PC, op),
            0xBB => NotImplemented(--_cpuState.PC, op),
            0xBC => NotImplemented(--_cpuState.PC, op),
            0xBD => NotImplemented(--_cpuState.PC, op),
            0xBE => NotImplemented(--_cpuState.PC, op),
            0xBF => NotImplemented(--_cpuState.PC, op),

            0xC0 => NotImplemented(--_cpuState.PC, op),
            0xC1 => NotImplemented(--_cpuState.PC, op),
            0xC2 => NotImplemented(--_cpuState.PC, op),
            0xC3 => NotImplemented(--_cpuState.PC, op),
            0xC4 => NotImplemented(--_cpuState.PC, op),
            0xC5 => NotImplemented(--_cpuState.PC, op),
            0xC6 => NotImplemented(--_cpuState.PC, op),
            0xC7 => NotImplemented(--_cpuState.PC, op),
            0xC8 => NotImplemented(--_cpuState.PC, op),
            0xC9 => NotImplemented(--_cpuState.PC, op),
            0xCA => NotImplemented(--_cpuState.PC, op),
            0xCB => NotImplemented(--_cpuState.PC, op),
            0xCC => NotImplemented(--_cpuState.PC, op),
            0xCD => NotImplemented(--_cpuState.PC, op),
            0xCE => NotImplemented(--_cpuState.PC, op),
            0xCF => NotImplemented(--_cpuState.PC, op),

            0xD0 => NotImplemented(--_cpuState.PC, op),
            0xD1 => NotImplemented(--_cpuState.PC, op),
            0xD2 => NotImplemented(--_cpuState.PC, op),
            0xD3 => NotImplemented(--_cpuState.PC, op),
            0xD4 => NotImplemented(--_cpuState.PC, op),
            0xD5 => NotImplemented(--_cpuState.PC, op),
            0xD6 => NotImplemented(--_cpuState.PC, op),
            0xD7 => NotImplemented(--_cpuState.PC, op),
            0xD8 => NotImplemented(--_cpuState.PC, op),
            0xD9 => NotImplemented(--_cpuState.PC, op),
            0xDA => NotImplemented(--_cpuState.PC, op),
            0xDB => NotImplemented(--_cpuState.PC, op),
            0xDC => NotImplemented(--_cpuState.PC, op),
            0xDD => NotImplemented(--_cpuState.PC, op),
            0xDE => NotImplemented(--_cpuState.PC, op),
            0xDF => NotImplemented(--_cpuState.PC, op),

            0xE0 => NotImplemented(--_cpuState.PC, op),
            0xE1 => NotImplemented(--_cpuState.PC, op),
            0xE2 => NotImplemented(--_cpuState.PC, op),
            0xE3 => NotImplemented(--_cpuState.PC, op),
            0xE4 => NotImplemented(--_cpuState.PC, op),
            0xE5 => NotImplemented(--_cpuState.PC, op),
            0xE6 => NotImplemented(--_cpuState.PC, op),
            0xE7 => NotImplemented(--_cpuState.PC, op),
            0xE8 => NotImplemented(--_cpuState.PC, op),
            0xE9 => NotImplemented(--_cpuState.PC, op),
            0xEA => NotImplemented(--_cpuState.PC, op),
            0xEB => NotImplemented(--_cpuState.PC, op),
            0xEC => NotImplemented(--_cpuState.PC, op),
            0xED => NotImplemented(--_cpuState.PC, op),
            0xEE => NotImplemented(--_cpuState.PC, op),
            0xEF => NotImplemented(--_cpuState.PC, op),

            0xF0 => NotImplemented(--_cpuState.PC, op),
            0xF1 => NotImplemented(--_cpuState.PC, op),
            0xF2 => NotImplemented(--_cpuState.PC, op),
            0xF3 => NotImplemented(--_cpuState.PC, op),
            0xF4 => NotImplemented(--_cpuState.PC, op),
            0xF5 => NotImplemented(--_cpuState.PC, op),
            0xF6 => NotImplemented(--_cpuState.PC, op),
            0xF7 => NotImplemented(--_cpuState.PC, op),
            0xF8 => NotImplemented(--_cpuState.PC, op),
            0xF9 => NotImplemented(--_cpuState.PC, op),
            0xFA => NotImplemented(--_cpuState.PC, op),
            0xFB => NotImplemented(--_cpuState.PC, op),
            0xFC => NotImplemented(--_cpuState.PC, op),
            0xFD => NotImplemented(--_cpuState.PC, op),
            0xFE => NotImplemented(--_cpuState.PC, op),
            0xFF => NotImplemented(--_cpuState.PC, op),
        };
    }

    private static int NotImplemented(ushort address, byte opcode) =>
        throw new NotImplementedException(
            $"Encountered un-implemented opcode ${opcode:X2} at address ${address:X4}");
}