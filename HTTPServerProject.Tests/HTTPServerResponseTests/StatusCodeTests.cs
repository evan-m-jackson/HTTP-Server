using HTTPServerResponse.StatusCode;

namespace HTTPServerProject.Tests;

public class UnitTestsForWritingResponses
{
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
}