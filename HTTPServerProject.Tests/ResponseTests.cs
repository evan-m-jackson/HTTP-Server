namespace HTTPServerProject.ResponseTests;

public class UnitTestsForResponses
{
    public List<string> eStream = new List<string>();

    [Theory]
    [InlineData(200, "HTTP/1.1 200 OK")]
    [InlineData(301, "HTTP/1.1 301 Moved Permanently")]
    [InlineData(404, "HTTP/1.1 404 Not Found")]
    [InlineData(405, "HTTP/1.1 405 Method Not Allowed")]
    public void GiveStatusCodeMessageTest(int code, string message)
    {
        var expected = message;
        var statusCode = new ResponseCode(code);
        var actual = statusCode.GetStatus();
        Assert.Equal(actual, expected);
    }

    [Fact]
    public void AddAndGetHeaderTest()
    {
        var expected = new List<string>() { "HTTP/1.1 200 OK", "Location: Earth", "", "Hello World!" };
        var writer = new TestWriteStreams(eStream);
        var body = "Hello World!";

        var response = new WriteResponse(writer, 200, body);
        response.AddHeader("Location", "Earth");
        response.GetResponse();
        Assert.Equal(eStream, expected);
    }


    [Fact]
    public void GiveResponseBodyTest()
    {
        var expected = new List<string>() { "HTTP/1.1 200 OK", "", "Hello World!" };
        var writer = new TestWriteStreams(eStream);
        var body = "Hello World!";

        var response = new WriteResponse(writer, 200, body);
        response.GetResponse();

        Assert.Equal(eStream, expected);
    }

}
