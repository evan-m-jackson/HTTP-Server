namespace HTTPServerProject.RequestTests;

public class UnitTestsForRequests
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
    public void GetRequestBodyTest()
    {
        var reader = new TestReadStreams(request);

        var header = new Header(reader);
        var initialLine = header.GetLine();
        var headers = header.GetHeaders();

        var body = new Body(reader);
        var expected = body.GetBody();

        Assert.Equal("quit", expected);
    }


}