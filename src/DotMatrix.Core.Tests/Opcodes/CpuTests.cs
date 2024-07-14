using DotMatrix.Core.Instructions;

namespace DotMatrix.Core.Tests.Opcodes;

public sealed class CpuTests
{
    // Uncomment to test specific opcodes for easier debugging
    // public static IEnumerable<object[]> GetTestData() => CpuTestData.GetTestData([ 0xCD ], "cd 02 0d");

    // Test all opcodes
    public static IEnumerable<object[]> GetTestData() => CpuTestData.GetTestData();

    private static void ExecuteTest(CpuTestData testData)
    {
        // Set up CPU
        LoggingBus bus = new(testData.Initial.Ram);
        CpuState initialState = testData.Initial.State with
        {
            Ir = testData.Opcode,
            Pc = (ushort)(testData.Initial.State.Pc + 1),
        };

        Cpu cpu = new(bus, new OpcodeHandler(), initialState);
        cpu.Run(CancellationToken.None, instructions: 1);

        VerifyCpuLogs(testData.GetCpuLog(), bus.Log);

        // Control for post-increment
        CpuState finalState = cpu.State with { Pc = (ushort)(cpu.State.Pc - 1) };
        finalState.Should().BeEquivalentTo(testData.Final.State,
            options => options
                .Excluding(o => o.Ir)
                .Excluding(o => o.InterruptMasterEnable)
                .Excluding(o => o.SetImeNext));
    }

    private static void VerifyCpuLogs(IEnumerable<CpuLog?> expected, IEnumerable<CpuLog> actual)
    {
        expected = expected.Where(e => e is not null);
        IEnumerable<(CpuLog Expected, CpuLog Actual)> logs = expected.Zip(actual)!;

        foreach ((CpuLog Expected, CpuLog Actual) log in logs)
        {
            log.Actual.Should().BeEquivalentTo(log.Expected);
        }
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void OpcodeTest(byte opcode)
    {
        IEnumerable<CpuTestData> tests = CpuTestData.GetTestDataForOpcode(opcode);
        foreach (CpuTestData test in tests)
        {
            ExecuteTest(test);
        }
    }
}
