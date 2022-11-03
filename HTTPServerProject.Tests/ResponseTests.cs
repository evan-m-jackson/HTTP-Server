namespace HTTPServerProject.ResponseTests;

public class UnitTestsForResponses
{
    public List<string> eStream = new List<string>();

    [Fact]
   public void GiveResponseHeaderTest()
   {
        var expected = new List<string>() {"HTTP/1.1 200 OK", ""};
        var writer = new TestWriteStreams(eStream);

        var response = new Response(writer);
        response.WriteResponseHeader();

        Assert.Equal(eStream, expected); 
   }

   [Fact]
   public void GiveResponseBodyTest()
   {
        var expected = new List<string>() {"HTTP/1.1 200 OK", "", "Hello World!"};
        var writer = new TestWriteStreams(eStream);
        var input = "Hello World!";

        var response = new Response(writer);
        response.WriteResponse(input);

        Assert.Equal(eStream, expected);
   }
}
