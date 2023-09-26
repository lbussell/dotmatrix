namespace DotMatrix.Core.Opcodes;

public class Instruction(IOpcode opcode, ushort? arg)
{
     public int Execute(ref CpuState cpuState, ushort? arg)
     {
         cpuState = opcode.Execute(cpuState, arg);
         return opcode.TCycles;
     }

     public override string ToString()
     {
         return opcode.Name + opcode.ReadType switch
         {
             ReadType.Read8 => $", ${(byte)arg!:X2}",
             ReadType.Read16 => $", ${(ushort)arg!:X4}",
             _ => string.Empty,
         };
     }
}
