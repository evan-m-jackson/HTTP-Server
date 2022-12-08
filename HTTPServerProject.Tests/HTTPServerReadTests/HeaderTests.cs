using HTTPServerReadTests.Streams;
using HTTPServerRead.Header;

namespace HTTPServerReadTests.HeaderTests;

public class UnitTestsForReadingHeaders
{
    public List<string> request = new List<string>() { "GET / HTTP/1.1", "Host: localhost:5000", "User-Agent: curl/7.79.1", "Accept: */*", "", "quit" };

    [Fact]
    public void GetRequestInitialLineTest()
    {
        var expected = "GET / HTTP/1.1";
        var reader = new TestReadStreams(request);

        var header = new Header(reader);
        var initialLine = header.GetLine();

        Assert.Equal(initialLine, expected);
    }

    [Fact]
    public void GetRequestTypeTest()
    {
        var reader = new TestReadStreams(request);

        var header = new Header(reader);
        var initialLine = header.GetLine();

        var type = header.GetRequestType(initialLine);

        Assert.Equal("GET", type);
    }

    [Fact]
    public void GetPathTest()
    {
        var requestLine = "GET /request HTTP/1.1";
        var reader = new TestReadStreams(request);

        var header = new Header(reader);

        var path = header.GetPath(requestLine);

        Assert.Equal("request", path);
    }

    [Fact]
    public void GetRequestHeadersTest()
    {
        var expected = new List<string>() { "Host: localhost:5000", "User-Agent: curl/7.79.1", "Accept: */*" };
        var reader = new TestReadStreams(request);

        var header = new Header(reader);
        var initialLine = header.GetLine();

        var headers = header.GetHeaders();

        Assert.Equal(headers, expected);
    }
    
    [Fact]
    public void GetStatusCodeNumberTest()
    {
        var proxyStream = new List<string>() { "HTTP/1.1 200 OK", "Allow: GET, OPTIONS", "", "Hello World" };
        var proxyReader = new TestReadStreams(proxyStream);
        var proxyResponseHeader = new Header(proxyReader);
        var proxyStatusLine = proxyResponseHeader.GetLine();
        var proxyStatusCode = proxyResponseHeader.GetCode(proxyStatusLine);

        Assert.Equal(200, proxyStatusCode);
    }

    [Fact]
    public void GetStatusCodeFromDirtyLineTest()
    {
        var proxyStream = new List<string>() { "[{'id':1},{'id':2},{'id':3},{'id':4}]HTTP/1.1 200 OK", "Allow: GET, OPTIONS", "", "Hello World" };
        var proxyReader = new TestReadStreams(proxyStream);
        var proxyResponseHeader = new Header(proxyReader);
        var proxyStatusLine = proxyResponseHeader.GetLine();
        var proxyStatusCode = proxyResponseHeader.GetCode(proxyStatusLine);

        Assert.Equal(200, proxyStatusCode);
    }
}