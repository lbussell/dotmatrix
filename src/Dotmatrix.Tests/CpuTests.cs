namespace Dotmatrix.Tests;

public class CpuTests
{
    [Fact]
    public void TestConstruction()
    {
        new DefaultServiceProvider().GetService<ICpu>()
            .Should().NotBeNull();
    }
}
