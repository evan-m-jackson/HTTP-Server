using HTTPServerRead.Header;
using HTTPServerRead.Body;
using HTTPServerReadTests.Streams;
using HTTPServerWriteTests.Streams;
using HTTPServerWrite.Request;
using HTTPServerProxyTests.Client;
using HTTPServerWrite.Response;
using HTTPServerProxy.Response;

namespace HTTPServerWriteTests.RequestTests;

public class UnitTestsWritingRequests
{

    public List<string> request = new List<string>() { "GET / HTTP/1.1", "Host: localhost:5000", "User-Agent: curl/7.79.1", "Accept: */*", "", "quit" };

    [Fact]
    public void GetProxyRequestTest()
    {
        List<string> expected = new List<string>() { "GET / HTTP/1.1", "Host: localhost:5000", "User-Agent: curl/7.79.1", "Accept: */*", "", "quit" };
        var reader = new TestReadStreams(request);
        var header = new Header(reader);
        var initialLine = header.GetLine();
        var reqHeaders = header.GetHeaders();
        var body = new Body(reader);
        var reqBody = body.GetBody();

        var proxyClient = new TestProxyClient();
        var proxyStream = proxyClient.GetStream();
        var writer = new TestWriteStreams(proxyStream);

        var proxyRequest = new WriteRequest(writer, initialLine, reqHeaders, reqBody);
        proxyRequest.GetRequest();

        Assert.Equal(expected, proxyStream);
    }

    [Fact]
    public void GetProxyResponseTest()
    {
        var expected = new List<string>() { "HTTP/1.1 200 OK", "Allow: GET, OPTIONS", "", "Hello World" };
        var stream = new List<string>();
        var proxyStream = new List<string>() { "HTTP/1.1 200 OK", "Allow: GET, OPTIONS", "", "Hello World" };
        var proxyReader = new TestReadStreams(proxyStream);
        
        var proxyResponseHeader = new Header(proxyReader);
        var proxyStatusLine = proxyResponseHeader.GetLine();
        var proxyStatusCode = proxyResponseHeader.GetCode(proxyStatusLine);
        var pRHeaders = proxyResponseHeader.GetHeaders();
        var proxyResponseBody = new Body(proxyReader);
        var pRBody = proxyResponseBody.GetBody();

        var writer = new TestWriteStreams(stream);
        var response = new WriteResponse(writer, proxyStatusCode, pRBody, pRHeaders);
        response.GetResponse();

        Assert.Equal(expected, stream);
    }
}