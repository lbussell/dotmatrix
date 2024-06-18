using System.Text.Json;
using Xunit.Abstractions;

namespace DotMatrix.Core.Tests;

using System.Collections.Generic;

public class CpuTests(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    public static IEnumerable<object[]> GetTestData(int opcode) => CpuTestData.GetTestData(opcode);

    private class LoggingBus(IDictionary<ushort, byte> ram) : IBus
    {
        private readonly IDictionary<ushort, byte> _ram = ram;

        public List<CpuLog> Log { get; } = [];

        public byte this[ushort address]
        {
            get => Get(address);
            set => Set(address, value);
        }

        private byte Get(ushort address)
        {
            byte value = _ram[address];
            Log.Add(new CpuLog(address, value, ActivityType.Read));
            return value;
        }

        private void Set(ushort address, byte value)
        {
            _ram[address] = value;
            Log.Add(new CpuLog(address, value, ActivityType.Write));
        }
    }

    private static void ExecuteTest(byte opcode, CpuTestData testData)
    {
        // set up cpu
        LoggingBus bus = new(testData.Initial.GetTestRam());
        CpuState initialState = testData.Initial.ToCpuState();
        initialState.Ir = opcode;
        Cpu cpu = new(bus, new OpcodeHandler(), initialState);

        int expectedTCycles = testData.Cycles.Length * 4;
        while (cpu.State.TCycles < expectedTCycles)
        {
            cpu.Step();
        }

        VerifyCpuLogs(testData.GetCpuLog(), bus.Log);
    }

    private static void VerifyCpuLogs(IEnumerable<CpuLog?> expected, IEnumerable<CpuLog> actual)
    {
        expected = expected.Where(e => e is not null);
        IEnumerable<(CpuLog Expected, CpuLog Actual)> logs = expected.Zip(actual)!;

        foreach ((CpuLog Expected, CpuLog Actual) log in logs)
        {
            log.Expected.Should().BeEquivalentTo(log.Actual);
        }
    }

    [Theory, MemberData(nameof(GetTestData), 0x00)]
    public void NoOp(byte opcode, CpuTestData testData) => ExecuteTest(opcode, testData);

    [Theory, MemberData(nameof(GetTestData), 0x40)]
    public void Load8(byte opcode, CpuTestData testData) => ExecuteTest(opcode, testData);
}
