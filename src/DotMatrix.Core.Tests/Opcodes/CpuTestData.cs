using System.Text.Json.Nodes;
using DotMatrix.Core.Tests.Model;

namespace DotMatrix.Core.Tests.Opcodes;

public sealed record CpuTestData(
    string Name,
    CpuTestState Initial,
    CpuTestState Final,
    JsonValue[]?[] Cycles)
{
    private const string TestDataDir = "Opcodes/CpuTestData/v2/";

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

    // Get all the opcodes for which there is a test file
    public static IEnumerable<object[]> GetTestData()
    {
        return GetTestDataInternal().Select(td => new object[] { td });
    }

    public static IEnumerable<CpuTestData> GetTestDataForOpcode(byte opcode)
    {
        var testData = ReadTestDataFromFile(opcode) ?? [];
        return testData.Select(td => td with { Opcode = opcode });
    }

    private static IEnumerable<byte> GetTestDataInternal()
    {
        return Enumerable.Range(0x00, 0xFF)
            .Select(op => (byte)op)
            .Where(op => File.Exists(GetTestFilePath(op)));
    }

    // public static IEnumerable<object[]> GetTestData(byte opcode)
    // {
    //     IEnumerable<CpuTestData> tests = GetTestDataInternal(opcodes);
    //
    //     if (name != null)
    //     {
    //         tests = tests.Where(td => td.Name == name);
    //     }
    //
    //     return tests.Select(testData => new object[] { testData });
    // }

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
            .Select(GetTestDataForOpcode)
            .SelectMany(td => td);

    private static IEnumerable<CpuTestData> ReadTestDataFromFile(byte opcode)
    {
        try
        {
            return CpuTestDataModel
                .FromJson(ReadTestDataFile(opcode))
                .Select(FromModel);
        }
        catch (FileNotFoundException)
        {
            return [];
        }
    }

    private static string ReadTestDataFile(byte opcode) =>
        File.ReadAllText(GetTestFilePath(opcode));

    private static string GetTestFilePath(byte opcode)
    {
        return Path.Combine(TestDataDir, $"{opcode:x2}.json");
    }
}
