using System.Text.Json;
using System.Text.Json.Nodes;

namespace DotMatrix.Core.Tests;

public record CpuTestData(
    string Name,
    CpuTestState Initial,
    CpuTestState Final,
    JsonValue[]?[] Cycles)
{
    private const string TestDataDir = "CpuTestData/v2/";

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    };

    public byte Opcode { get; set; }

    public static IEnumerable<object[]> GetTestData(int opcode) =>
        GetTestDataInternal((byte)opcode).Select(d => new object[] { (byte)opcode, d });

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

    private static ActivityType GetActivityType(string type) => type switch
    {
        "read" => ActivityType.Read,
        "write" => ActivityType.Write,
        _ => throw new ArgumentException(type),
    };

    private static IEnumerable<CpuTestData> GetTestDataInternal(byte opcode) =>
        ReadTestDataFromFile(opcode);

    private static IEnumerable<CpuTestData> ReadTestDataFromFile(byte opcode)
    {
        string filePath = GetTestFilePath(opcode);
        Console.WriteLine($"r {filePath}");
        IEnumerable<CpuTestData> data = JsonSerializer.Deserialize<IEnumerable<CpuTestData>>(File.ReadAllText(filePath), JsonOptions)
            ?? throw new Exception($"Got null when deserializing {filePath}");
        return data;
    }

    private static string GetTestFilePath(byte opcode) =>
        Path.Combine(TestDataDir, $"{opcode:x2}.json");
};
