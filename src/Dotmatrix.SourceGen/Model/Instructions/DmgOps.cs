using System.Runtime.Serialization;
using System.Text.Json;

namespace DotMatrix.SourceGen.Model.Instructions;

public record DmgOps(IEnumerable<Opcode> Unprefixed, IEnumerable<Opcode> CBPrefixed)
{
    public static DmgOps FromJson(string? json)
    {
        if (json == null)
        {
            throw new SerializationException($"Got null input when deserializing {nameof(DmgOps)}.");
        }

        return JsonSerializer.Deserialize<DmgOps>(json, DmgOpsConverter.Settings)
            ?? throw new SerializationException($"Got null output when deserializing {nameof(DmgOps)}.");
    }
};
