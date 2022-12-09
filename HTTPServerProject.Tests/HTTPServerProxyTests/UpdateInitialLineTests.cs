using HTTPServerProject.Update.Initial.Line;
using HTTPServerProxyTests.FirstID;
using HTTPServerReadTests.Streams;
using HTTPServerWriteTests.Streams;

namespace HTTPServerProject.Tests.Update.Initial.Line;

public class InitialLineTests
{
    [Theory]
    [InlineData("todo/1", "PUT", "PUT /todo/4 HTTP/1.1")]
    [InlineData("todo/2", "PUT", "PUT /todo/2 HTTP/1.1")]
    [InlineData("todo/1", "DELETE", "DELETE /todo-delete/4 HTTP/1.1")]
    public void GetTodoFirstIDInitialLineTest(string path, string method, string expected)
    {
        var stream = new List<string>();
        var proxyReader = new TestReadStreams(stream);
        var proxyWriter = new TestWriteStreams(stream);

        var firstID = new TestGetFirstID(proxyReader, proxyWriter, path, method);
        var updateIL = new UpdateInitialLine(proxyReader, proxyWriter, path, method, firstID);
        var actual = updateIL.Run();

        Assert.Equal(expected, actual);
    }
}