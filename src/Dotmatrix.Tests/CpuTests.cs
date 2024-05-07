namespace DotMatrix.Tests;

public class CpuTests
{
    [Theory]
    [InlineData(new byte[] { }, new byte[] { })]
    [InlineData(new byte[] { }, null)]
    [InlineData(null, new byte[] { })]
    public void TestConstruction(byte[]? bios, byte[]? cart)
    {
        GameConsoleFactory.Create(bios, cart)
            .Should().NotBeNull();
    }
}
