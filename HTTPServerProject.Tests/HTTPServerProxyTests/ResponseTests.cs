using HTTPServerProxy.Response;

namespace HTTPServerProject.Tests;

public class ProxyResponseTests
{
    [Theory]
    [MemberData(nameof(Data))]
    public void ProxyResponseUnitTest(List<string> pStream, string type, string path, string line)
    {
        var proxyStream = pStream;
        var reader = new TestReadStreams(proxyStream);
        var stream = new List<string>();
        var writer = new TestWriteStreams(stream);

        var httpType = type;
        var httpPath = path;

        var proxyResponse = new ProxyResponse(reader: reader, writer: writer, path: httpPath, type: httpType);
        proxyResponse.Run();

        var actualStatusLine = stream[0];
        var expectedStatusLine = line;
        Assert.Equal(expectedStatusLine, actualStatusLine);
    }

    public static IEnumerable<object[]> Data()
    {
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Content-Type: application/json",
	"Content-Length: 20",
	"",
	"{ 'task': 'First Item' }"
	}, "POST", "todo", "HTTP/1.1 201 Created"};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Content-Type: application/json",
	"Content-Length: 20",
	"",
	""
    	}, "POST", "todo", "HTTP/1.1 201 Created"};
	yield return new object[] {new List<string>(){"HTTP/1.1 415 Unsupported Media Type", ""}, "POST", "todo", "HTTP/1.1 415 Unsupported Media Type"};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Content-Type: application/json",
	"Content-Length: 2", 
	"", 
	"a new task"
	}, "POST", "todo", "HTTP/1.1 400 Bad Request"};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Content-Type: application/json",
	"Content-Length: 20",
	"",
	"{ 'task': 'First Item' }"
	}, "PUT", "todo/1", "HTTP/1.1 200 OK"};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Content-Type: application/json",
	"Content-Length: 11",
	"",
	""
	}, "PUT", "todo/1", "HTTP/1.1 200 OK"};
	yield return new object[] {new List<string>() { "HTTP/1.1 415 Unsupported Media Type", "" }, "PUT", "todo/1", "HTTP/1.1 415 Unsupported Media Type"};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Content-Type: application/json",
	"Content-Length: 2",
	"",
	"{}"
	}, "PUT", "todo/1", "HTTP/1.1 400 Bad Request"};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Content-Type: application/json",
	"",
	"Task deleted successfully."
	}, "DELETE", "todo/1", "HTTP/1.1 204 No Content"};
	yield return new object[] {new List<string>() { "HTTP/1.1 500 Internal Server Error", "" }, "DELETE", "todo/1000", "HTTP/1.1 204 No Content"};
    }
}
