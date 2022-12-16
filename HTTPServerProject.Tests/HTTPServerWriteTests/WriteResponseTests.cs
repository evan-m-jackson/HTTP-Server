using HTTPServerWriteTests.Streams;
using HTTPServerWrite.Response;

namespace HTTPServerWriteTests.ResponseTests;

public class UnitTestsForWritingResponses
{
    public List<string> eStream = new List<string>();

    [Fact]
    public void WriteResponseTest()
    {
        var expected = new List<string>()
        {
            "HTTP/1.1 200 OK", 
            "", 
            "Hello World!"
        };
        var writer = new TestWriteStreams(eStream);
        var body = "Hello World!";

        var response = new WriteResponse(writer: writer, code: 200, body: body);
        response.GetResponse();

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void WriteResponseWithHeaderTest()
    {
        var expected = new List<string>()
        {
            "HTTP/1.1 200 OK", 
            "Location: Earth", 
            "", 
            "Hello World!"
        };
        var writer = new TestWriteStreams(eStream);
        var headers = new List<string>() { "Location: Earth" };
        var body = "Hello World!";

        var response = new WriteResponse(writer: writer, code: 200, body: body, headers: headers);
        response.GetResponse();

        Assert.Equal(eStream, expected);
    }
}