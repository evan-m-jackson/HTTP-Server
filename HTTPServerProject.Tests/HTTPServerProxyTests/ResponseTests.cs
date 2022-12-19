using HTTPServerReadTests.Streams;
using HTTPServerProxy.Response;
using HTTPServerWriteTests.Streams;

namespace HTTPServerProxyTests.Response;

public class ProxyResponseTests
{
    [Fact]
    public void GetTodoPOSTValidResponseTest()
    {
        var expectedStatusLine = "HTTP/1.1 201 Created";
        var proxyStream = new List<string>() 
		{
        "HTTP/1.1 200 OK", 
		"Content-Type: application/json", 
		"Content-Length: 20", 
		"", 
		"{ 'task': 'First Item' }" 
		};
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "POST";
        var httpPath = "todo";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader: reader, writer: writer, path: httpPath, type: httpType);
        proxyResponse.GetResponse();
        var actualStatusLine = stream[0];
        Assert.Equal(expectedStatusLine, actualStatusLine);
    }

    [Fact]
    public void GetTodoPOSTValidResponseNoBodyTest()
    {
        var expectedStatusLine = "HTTP/1.1 201 Created";
        var proxyStream = new List<string>() 
		{ 
			"HTTP/1.1 200 OK", 
			"Content-Type: application/json", 
			"Content-Length: 20", 
			"", 
			"" 
		};
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "POST";
        var httpPath = "todo";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader: reader, writer: writer, path: httpPath, type: httpType);
        proxyResponse.GetResponse();
        var actualStatusLine = stream[0];
        Assert.Equal(expectedStatusLine, actualStatusLine);
    }

    [Fact]
    public void GetTodoPOSTUnsupportedMediaTest()
    {
        var expectedStatusLine = "HTTP/1.1 415 Unsupported Media Type";
        var proxyStream = new List<string>() { "HTTP/1.1 415 Unsupported Media Type", "" };
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "POST";
        var httpPath = "todo";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader: reader, writer: writer, path: httpPath, type: httpType);
        proxyResponse.GetResponse();
        var actualStatusLine = stream[0];
        Assert.Equal(expectedStatusLine, actualStatusLine);
    }

    [Fact]
    public void GetTodoPOSTInvalidValuesTest()
    {
        var expectedStatusLine = "HTTP/1.1 400 Bad Request";
        var proxyStream = new List<string>() 
		{ 
			"HTTP/1.1 200 OK", 
			"Content-Type: application/json", 
			"Content-Length: 2", "", "a new task" 
		};
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "POST";
        var httpPath = "todo";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader: reader, writer: writer, path: httpPath, type: httpType);
        proxyResponse.GetResponse();
        var actualStatusLine = stream[0];
        Assert.Equal(expectedStatusLine, actualStatusLine);
    }

    [Fact]
    public void GetTodoPUTValidResponseTest()
    {
        var expectedStatusLine = "HTTP/1.1 200 OK";
        var proxyStream = new List<string>() 
		{ 
			"HTTP/1.1 200 OK", 
			"Content-Type: application/json", 
			"Content-Length: 20", 
			"", 
			"{ 'task': 'First Item' }" 
		};
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "PUT";
        var httpPath = "todo/1";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader: reader, writer: writer, path: httpPath, type: httpType);
        proxyResponse.GetResponse();
        var actualStatusLine = stream[0];
        Assert.Equal(expectedStatusLine, actualStatusLine);
    }

    [Fact]
    public void GetTodoPUTValidResponseNoBodyTest()
    {
        var expectedStatusLine = "HTTP/1.1 200 OK";
        var proxyStream = new List<string>() 
		{ 
			"HTTP/1.1 200 OK", 
			"Content-Type: application/json", 
			"Content-Length: 11", 
			"", 
			"" 
		};
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "PUT";
        var httpPath = "todo/1";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader: reader, writer: writer, path: httpPath, type: httpType);
        proxyResponse.GetResponse();
        var actualStatusLine = stream[0];
        Assert.Equal(expectedStatusLine, actualStatusLine);
    }

    [Fact]
    public void GetTodoPUTUnsupportedMediaTest()
    {
        var expectedStatusLine = "HTTP/1.1 415 Unsupported Media Type";
        var proxyStream = new List<string>() { "HTTP/1.1 415 Unsupported Media Type", "" };
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "PUT";
        var httpPath = "todo/1";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader: reader, writer: writer, path: httpPath, type: httpType);
        proxyResponse.GetResponse();
        var actualStatusLine = stream[0];
        Assert.Equal(expectedStatusLine, actualStatusLine);
    }

    [Fact]
    public void GetTodoPUTInvalidValuesTest()
    {
        var expectedStatusLine = "HTTP/1.1 400 Bad Request";
        var proxyStream = new List<string>() 
		{ 
			"HTTP/1.1 200 OK", 
			"Content-Type: application/json", 
			"Content-Length: 2", 
			"", 
			"{}" 
		};
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "PUT";
        var httpPath = "todo/1";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader: reader, writer: writer, path: httpPath, type: httpType);
        proxyResponse.GetResponse();
        var actualStatusLine = stream[0];
        Assert.Equal(expectedStatusLine, actualStatusLine);
    }

    [Fact]
    public void GetTodoDELETEValidTest()
    {
        var expectedStatusLine = "HTTP/1.1 204 No Content";
        var proxyStream = new List<string>() 
		{ 
			"HTTP/1.1 200 OK", 
			"Content-Type: application/json", 
			"", 
			"Task deleted successfully." 
		};
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "DELETE";
        var httpPath = "todo/1";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader: reader, writer: writer, path: httpPath, type: httpType);
        proxyResponse.GetResponse();
        var actualStatusLine = stream[0];
        Assert.Equal(expectedStatusLine, actualStatusLine);
    }

    [Fact]
    public void GetToDoDELETEInvalidTest()
    {
        var expectedStatusLine = "HTTP/1.1 204 No Content";
        var proxyStream = new List<string>() { "HTTP/1.1 500 Internal Server Error", "" };
        var stream = new List<string>();
        var reader = new TestReadStreams(proxyStream);
        var httpType = "DELETE";
        var httpPath = "todo/1000";
        var writer = new TestWriteStreams(stream);

        var proxyResponse = new ProxyResponse(reader: reader, writer: writer, path: httpPath, type: httpType);
        proxyResponse.GetResponse();
        var actualStatusLine = stream[0];
        Assert.Equal(expectedStatusLine, actualStatusLine);
    }
}