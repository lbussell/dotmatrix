using DotMatrix.Core.Instructions;

namespace DotMatrix.Core;

public static class Util
{
    private const int NumberOfOpcodes = 0xFF;

    public static IEnumerable<int> GetImplementedOpcodesList()
    {
        return GetImplementedOpcodesArray()
            .Select((isEnabled, opcode) => (isEnabled, opcode))
            .Where(data => data.isEnabled)
            .Select(data => data.opcode);
    }

    public static IEnumerable<bool> GetImplementedOpcodesArray()
    {
        OpcodeHandler handler = new();
        IBus bus = new DummyBus();
        CpuState bogusCpuState = new();

        for (int i = 0; i <= NumberOfOpcodes; i += 1)
        {
            bogusCpuState.Ir = (byte)i;

            bool result = false;
            try
            {
                handler.HandleOpcode(ref bogusCpuState, bus);
                result = true;
            }
            catch (NotImplementedException _)
            {
            }

            yield return result;
        }
    }

    public static IEnumerable<bool> GetAnticipatedOpcodesArray()
    {
        OpcodeHandler handler = new();
        IBus bus = new DummyBus();
        CpuState bogusCpuState = new();

        for (int i = 0; i <= NumberOfOpcodes; i += 1)
        {
            bogusCpuState.Ir = (byte)i;

            bool result = false;
            try
            {
                handler.HandleOpcode(ref bogusCpuState, bus);
                result = true;
            }
            catch (NotImplementedException e)
            {
                if (!e.Message.Contains("Unexpected"))
                {
                    result = true;
                }
            }

            yield return result;
        }
    }

    private class DummyBus : IBus
    {
        public byte this[ushort address]
        {
            get => 0;
            set { }
        }
    }
}
