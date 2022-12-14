using HTTPServerRead.Header;
using HTTPServerRead.Body;

namespace HTTPServerProject.Tests;

public class UnitTestsForReadingBody
{
    public List<string> request = new List<string>() { "This is the body" };

    [Fact]
    public void GetRequestBodyTest()
    {
        var reader = new TestReadStreams(request);
        var body = new Body(reader);
        var actual = body.GetBody();

        Assert.Equal("This is the body", actual);
    }
}