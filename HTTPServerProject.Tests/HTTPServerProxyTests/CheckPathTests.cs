using HTTPServerProxy.Check.Paths;

namespace HTTPServerProject.Tests;

public class HttpPathTests
{
    [Theory]
    [InlineData("todo", true)]
    [InlineData("tod", false)]
    public void IsTodoPathValidTest(string path, bool expected)
    {
        var httpPath = new CheckPath();
        var actual = httpPath.IsTodoPath(path);
        Assert.Equal(expected, actual);
    }
}