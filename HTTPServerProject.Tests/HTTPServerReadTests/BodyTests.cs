using HTTPServerReadTests.Streams;
using HTTPServerRead.Header;
using HTTPServerRead.Body;

namespace HTTPServerReadTests.BodyTest;

public class UnitTestsForReadingBody
{
    public List<string> request = new List<string>() { "GET / HTTP/1.1", "Host: localhost:5000", "User-Agent: curl/7.79.1", "Accept: */*", "", "quit" };
    
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