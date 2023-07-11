using System.Text.RegularExpressions;

namespace Circular.Tests;

public class CircularTests
{
    [Fact]
    public void Test1()
    {
        Assert.True(8 == 32>>2);
    }
}