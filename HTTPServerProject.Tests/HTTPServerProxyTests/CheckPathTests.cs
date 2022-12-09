using HTTPServerProxy.Check.Paths;

namespace HTTPServerProxyTests.Check.Paths;

public class HttpPathTests
{
    [Theory]
    [InlineData("todo", true)]
    [InlineData("tod", false)]
    public void GetTodoPathTrueTest(string path, bool expected)
    {
        var httpPath = new CheckPath();
        var actual = httpPath.IsTodoPath(path);
        Assert.Equal(expected, actual);
    }
}