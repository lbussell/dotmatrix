namespace DotMatrix.Tests;

public class IntegrationTests
{
    [Fact]
    public void TestConsoleConstruction()
    {
        new DefaultServiceProvider().GetService<IGameConsole>()
            .Should().NotBeNull();
    }
}
