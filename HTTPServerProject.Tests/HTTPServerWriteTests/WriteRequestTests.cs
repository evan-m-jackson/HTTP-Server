using HTTPServerRead.Header;
using HTTPServerRead.Body;
using HTTPServerWrite.Request;
using HTTPServerWrite.Response;
using HTTPServerProxy.Response;

namespace HTTPServerProject.Tests;

public class UnitTestsWritingRequests
{

    public List<string> request = new List<string>()
    {
        "GET / HTTP/1.1",
        "Host: localhost:5000",
        "User-Agent: curl/7.79.1",
        "Accept: */*",
        "",
        "quit"
    };

    [Fact]
    public void WriteRequestTest()
    {
        List<string> expected = new List<string>()
        {
            "GET / HTTP/1.1",
            "Host: localhost:5000",
            "User-Agent: curl/7.79.1",
            "Accept: */*",
            "",
            "quit"
        };
        var reader = new TestReadStreams(request);
        var header = new Header(reader);
        var initialLine = header.GetLine();
        var reqHeaders = header.GetHeaders();
        var body = new Body(reader);
        var reqBody = body.GetBody();

        var proxyStream = new List<string>();
        var writer = new TestWriteStreams(proxyStream);

        var proxyRequest = new WriteRequest(writer: writer, initialLine: initialLine, headers: reqHeaders, body: reqBody);
        proxyRequest.Run();

        Assert.Equal(expected, proxyStream);
    }
}