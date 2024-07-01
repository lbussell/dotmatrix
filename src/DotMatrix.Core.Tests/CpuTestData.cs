using System.Text.Json.Nodes;
using DotMatrix.Core.Tests.Model;

namespace DotMatrix.Core.Tests;

public record CpuTestData(
    string Name,
    CpuTestState Initial,
    CpuTestState Final,
    JsonValue[]?[] Cycles)
{
    private const string TestDataDir = "CpuTestData/v2/";

    public byte Opcode { get; private init; }

    public IEnumerable<CpuLog?> GetCpuLog() =>
        Cycles.Select(cycleData =>
        {
            if (cycleData == null)
            {
                return null;
            }

            ushort address = (ushort)cycleData[0].GetValue<int>();
            byte value = (byte)cycleData[1].GetValue<int>();
            ActivityType type = GetActivityType(cycleData[2].GetValue<string>());
            return new CpuLog(address, value, type);
        });

    public static IEnumerable<object[]> GetTestData(IEnumerable<int> opcodes, string? name = null)
    {
        IEnumerable<CpuTestData> tests = GetTestDataInternal(opcodes);

        if (name != null)
        {
            tests = tests.Where(td => td.Name == name);
        }

        return tests.Select(testData => new object[] { testData });
    }

    private static CpuTestData FromModel(CpuTestDataModel model)
    {
        CpuTestState initial = CpuTestState.FromModel(model.Initial);

        int finalCycles = model.Cycles.Length * 4;
        CpuTestState final = CpuTestState.FromModel(model.Final, finalCycles);

        return new CpuTestData(model.Name, initial, final, model.Cycles);
    }

    private static ActivityType GetActivityType(string type) => type switch
        {
            "read" => ActivityType.Read,
            "write" => ActivityType.Write,
            _ => throw new ArgumentException(type),
        };

    private static IEnumerable<CpuTestData> GetTestDataInternal(IEnumerable<int> opcodes) =>
        opcodes
            .Select(o => (byte)o)
            .Select(GetTestDataInternal)
            .SelectMany(td => td);

    private static IEnumerable<CpuTestData> GetTestDataInternal(byte opcode) =>
        ReadTestDataFromFile(opcode)
            .Select(testData => testData with { Opcode = opcode });

    private static IEnumerable<CpuTestData> ReadTestDataFromFile(byte opcode)
    {
        return CpuTestDataModel
            .FromJson(ReadTestDataFile(opcode))
            .Select(FromModel);
    }

    private static string ReadTestDataFile(byte opcode) =>
        File.ReadAllText(Path.Combine(TestDataDir, $"{opcode:x2}.json"));
}
