using System.Text.Json;
using System.Text.Json.Nodes;

namespace DotMatrix.Core.Tests.Model;

public record CpuTestDataModel(
    string Name,
    CpuTestStateModel Initial,
    CpuTestStateModel Final,
    JsonValue[]?[] Cycles)
{
    private static readonly JsonSerializerOptions s_jsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    };

    public static IEnumerable<CpuTestDataModel> FromJson(string text) =>
        JsonSerializer.Deserialize<IEnumerable<CpuTestDataModel>>(text, s_jsonOptions)
        ?? throw new Exception($"Got null when deserializing CPU test data.");

};
