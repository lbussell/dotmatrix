// <copyright file="OpcodeNotFoundException.cs" company="PlaceholderCompany">Copyright (c) PlaceholderCompany. All rights reserved.</copyright>

namespace DotMatrix.Core.Opcodes;

public class OpcodeNotFoundException(byte opcode) : NotImplementedException($"Opcode for ${opcode:X2} not found.")
{
}
