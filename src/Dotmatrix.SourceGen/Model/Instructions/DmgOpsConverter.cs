using System.Text.Json;

namespace DotMatrix.SourceGen.Model.Instructions;

internal static class DmgOpsConverter
{
    public static readonly JsonSerializerOptions Settings = new(JsonSerializerDefaults.General)
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
}
