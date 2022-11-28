using HTTPServerProject.Tests.ReadStream;
using HTTPServerProject.Proxy.Response;
using HTTPServerProject.Tests.WriteStream;

namespace HTTPServerProject.Tests.Proxy.Response;

public class UnitTestsForProxyResponse
{
    [Fact]
    public void GetTodoValidResponseTest()
    {
        var expected = new List<string>() { "HTTP/1.1 201 Created", "Content-Type: application/json;charset=utf-8", "", "{ 'task': 'First Item' }" };
        var proxyStream = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: application/json", "", "{ 'id': 16, 'task': 'First Item' }" };
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "POST";
        var httpPath = "todo";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader, writer, httpPath, httpType);
        proxyResponse.GetResponse();

        Assert.Equal(expected, stream);
    }
    
    [Fact]
    public void GetTodoUnsupportedMediaTest()
    {
        var expected = new List<string>() { "HTTP/1.1 415 Unsupported Media Type", ""};
        var proxyStream = new List<string>() { "HTTP/1.1 415 Unsupported Media Type", ""};
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "POST";
        var httpPath = "todo";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader, writer, httpPath, httpType);
        proxyResponse.GetResponse();

        Assert.Equal(expected, stream);
    }

    [Fact]
    public void GetTodoInvalidValuesTest()
    {
        var expected = new List<string>() { "HTTP/1.1 400 Bad Request", ""};
        var proxyStream = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: application/json", "", "a new task" };
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "POST";
        var httpPath = "todo";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader, writer, httpPath, httpType);
        proxyResponse.GetResponse();

        Assert.Equal(expected, stream);
    }
    
    
}