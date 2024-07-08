namespace DotMatrix.Core.Tests.Opcodes;

public sealed record CpuLog(ushort Address, byte Value, ActivityType Type);
