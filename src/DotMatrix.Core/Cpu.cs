namespace DotMatrix.Core;

using System.Reflection;
using System.Runtime.CompilerServices;
using DotMatrix.Core.Opcodes;

public sealed class Cpu(Bus bus, IDisplay display)
{
    private const int CyclesPerFrame = DotMatrixConsoleSpecs.CpuSpeed / DotMatrixConsoleSpecs.FramesPerSecond;

    private readonly Dictionary<byte, IInstruction> _instructions = RegisterOpcodes();
    private CpuState _cpuState;
    private int _cyclesSinceLastFrame = 0;
    private IInstruction _notImplementedInstruction = new NotImplemented();

    public CpuState CpuState => _cpuState;

    public long Cycles { get; private set; } = 0;

    public void ExecuteFrame(int cycles = CyclesPerFrame)
    {
        while (_cyclesSinceLastFrame < cycles)
        {
            int elapsedCycles = ExecuteCycle();
            /* Tick PPU/APU with elapsedCycles here */
            _cyclesSinceLastFrame += elapsedCycles;
            Cycles += elapsedCycles;
        }

        /* Read inputs */
        display.RequestRefresh();
        _cyclesSinceLastFrame %= CyclesPerFrame;
    }

    private int ExecuteCycle()
    {
        byte opcode = bus.ReadInc8(ref _cpuState.PC);
        CpuUtil.Print(opcode);

        IInstruction instruction = _instructions.GetValueOrDefault(opcode) ?? _notImplementedInstruction;
        _cpuState = instruction.Execute(_cpuState, bus);
        CpuUtil.Print(_cpuState);

        return instruction.TCycles;
    }

    private static Dictionary<byte, IInstruction> RegisterOpcodes()
    {
        Dictionary<byte, IInstruction> instructions = new();

        foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
        {
            IEnumerable<Attribute> attributes = type.GetCustomAttributes(typeof(OpcodeAttribute));
            foreach (Attribute attribute in attributes)
            {
                OpcodeAttribute opcodeAttribute = (OpcodeAttribute)attribute;
                IInstruction instruction = opcodeAttribute.R != CpuRegister.Implied
                    ? (IInstruction)Activator.CreateInstance(type, opcodeAttribute.R)!
                    : (IInstruction)Activator.CreateInstance(type)!;

                instructions[opcodeAttribute.Opcode] = instruction;
                Console.WriteLine($"Registered opcode 0x{opcodeAttribute.Opcode:X2}: {instruction.Name}");
            }
        }

        return instructions;
    }
}
