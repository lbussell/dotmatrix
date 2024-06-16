using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using Xunit.Abstractions;

namespace DotMatrix.Core.Tests;

using System.Collections.Generic;

public class CpuTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private const string TestDataDir = "CpuTestData/v2/";

    private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    };

    public CpuTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    public static IEnumerable<object[]> GetTestData() => GetTestDataInternal();

    private static IEnumerable<object[]> GetTestDataInternal() =>
        GetAllTestData()
            .SelectMany(d => d)
            .Select(d => new []{ d });

    private static IEnumerable<IEnumerable<CpuTestData>> GetAllTestData() =>
        Directory.GetFiles(TestDataDir).Select(ReadTestDataFromFile);

    private static IEnumerable<CpuTestData> ReadTestDataFromFile(string filePath)
    {
        Console.WriteLine($"r {filePath}");
        return JsonSerializer.Deserialize<IEnumerable<CpuTestData>>(File.ReadAllText(filePath), jsonOptions)
            ?? throw new Exception($"Got null when deserializing {filePath}");
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Test1(CpuTestData testData)
    {
        _testOutputHelper.WriteLine(testData.Name);
    }
}

public record CpuTestData(
    string Name,
    CpuTestState Initial,
    CpuTestState Final,
    JsonValue[][] Cycles);

public record CpuTestState(
    byte A,
    byte B,
    byte C,
    byte D,
    byte E,
    byte F,
    byte H,
    byte L,
    ushort Pc,
    ushort Sp,
    int[][] Ram);
