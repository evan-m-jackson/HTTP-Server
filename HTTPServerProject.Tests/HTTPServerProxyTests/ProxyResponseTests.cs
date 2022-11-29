using HTTPServerProject.Tests.ReadStream;
using HTTPServerProject.Proxy.Response;
using HTTPServerProject.Tests.WriteStream;

namespace HTTPServerProject.Tests.Proxy.Response;

public class UnitTestsForProxyResponse
{
    [Fact]
    public void GetTodoPOSTValidResponseTest()
    {
        var expected = new List<string>() { "HTTP/1.1 201 Created", "Content-Type: application/json;charset=utf-8", "", "{ 'task': 'First Item' }" };
        var proxyStream = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: application/json", "", "{ 'id': 1, 'task': 'First Item' }" };
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
    public void GetTodoPOSTUnsupportedMediaTest()
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
    public void GetTodoPOSTInvalidValuesTest()
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
    
    [Fact]
    public void GetTodoPUTValidResponseTest()
    {
        var expected = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: application/json;charset=utf-8", "", "{ 'task': 'First Item' }" };
        var proxyStream = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: application/json", "", "{ 'id': 1, 'task': 'First Item' }" };
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "PUT";
        var httpPath = "todo/1";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader, writer, httpPath, httpType);
        proxyResponse.GetResponse();

        Assert.Equal(expected, stream);
    }
    
    [Fact]
    public void GetTodoPUTUnsupportedMediaTest()
    {
        var expected = new List<string>() { "HTTP/1.1 415 Unsupported Media Type", ""};
        var proxyStream = new List<string>() { "HTTP/1.1 415 Unsupported Media Type", ""};
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "PUT";
        var httpPath = "todo/1";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader, writer, httpPath, httpType);
        proxyResponse.GetResponse();

        Assert.Equal(expected, stream);
    }
    
    [Fact]
    public void GetTodoPUTInvalidValuesTest()
    {
        var expected = new List<string>() { "HTTP/1.1 400 Bad Request", ""};
        var proxyStream = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: application/json", "", "{}" };
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "PUT";
        var httpPath = "todo/1";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader, writer, httpPath, httpType);
        proxyResponse.GetResponse();

        Assert.Equal(expected, stream);
    }

    [Fact]
    public void GetTodoDELETEValidTest()
    {
        var expected = new List<string>() { "HTTP/1.1 204 No Content", ""};
        var proxyStream = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: application/json", "", "Task deleted successfully." };
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "DELETE";
        var httpPath = "todo/1";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader, writer, httpPath, httpType);
        proxyResponse.GetResponse();

        Assert.Equal(expected, stream);
    }

    [Fact]
    public void GetToDoDELETEInvalidTest()
    {
        var expected = new List<string>() { "HTTP/1.1 204 No Content", ""};
        var proxyStream = new List<string>() { "HTTP/1.1 500 Internal Server Error", "" };
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "DELETE";
        var httpPath = "todo/1000";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader, writer, httpPath, httpType);
        proxyResponse.GetResponse();

        Assert.Equal(expected, stream);
    }

    [Fact]
    public void GetToDoDELETE404Test()
    {
        var expected = new List<string>() { "HTTP/1.1 404 Not Found", ""};
        var proxyStream = new List<string>() { "HTTP/1.1 404 Not Found", "" };
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "DELETE";
        var httpPath = "todo/1000";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader, writer, httpPath, httpType);
        proxyResponse.GetResponse();

        Assert.Equal(expected, stream);
    }
}