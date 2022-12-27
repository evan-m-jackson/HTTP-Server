using HTTPServerProxy.FirstID;

namespace HTTPServerProject.Tests;

public class FirstIDTests
{
    [Theory]
    [InlineData("4", "[{ 'id': 4 }]")]
    [InlineData("1", "[]")]
    public void GetFirstIndexFromTodoListTest(string expected, string body)
    {
        var firstID = new GetFirstID(body);
        var actual = firstID.Run();

        Assert.Equal(expected, actual);
    }
}