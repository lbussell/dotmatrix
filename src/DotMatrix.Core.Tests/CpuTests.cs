using System.Text.Json;
using Xunit.Abstractions;

namespace DotMatrix.Core.Tests;

using System.Collections.Generic;

public class CpuTests(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    // Uncomment to test specific opcodes for easier debugging
    // public static IEnumerable<object[]> GetTestData() => CpuTestData.GetTestData([ 0x0e ]);

    // Test all opcodes
    public static IEnumerable<object[]> GetTestData() => CpuTestData.GetTestData(GetImplementedOpcodes());
    private static IEnumerable<int> GetImplementedOpcodes() => Util.GetImplementedOpcodesList();

    private static void ExecuteTest(CpuTestData testData)
    {
        // set up cpu
        LoggingBus bus = new(testData.Initial.Ram);
        CpuState initialState = testData.Initial.State with { Ir = testData.Opcode };
        Cpu cpu = new(bus, new OpcodeHandler(), initialState);

        int expectedTCycles = testData.Cycles.Length * 4;
        while (cpu.State.TCycles < expectedTCycles)
        {
            cpu.Step();
        }

        VerifyCpuLogs(testData.GetCpuLog(), bus.Log);
        cpu.State.Should().BeEquivalentTo(testData.Final.State,
            options => options.Excluding(o => o.Ir));
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

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void OpcodeTest(CpuTestData testData) => ExecuteTest(testData);
}
