using HTTPServerProject.Update.Initial.Line;
using HTTPServerProxy.FirstID;

namespace HTTPServerProject.Tests;

public class UpdateInitialLineTests
{
    [Theory]
    [InlineData("todo/1", "PUT", "PUT /todo/4 HTTP/1.1")]
    [InlineData("todo/2", "PUT", "PUT /todo/2 HTTP/1.1")]
    [InlineData("todo/1", "DELETE", "DELETE /todo-delete/4 HTTP/1.1")]
    public void GetTodoFirstIDInitialLineTest(string path, string method, string expected)
    {
        var todoList = "[{ 'id': 4 }]";
        var firstID = new GetFirstID(todoList);
        var updateIL = new UpdateInitialLine(path: path, type: method, firstId: firstID);
        var actual = updateIL.Run();

        Assert.Equal(expected, actual);
    }
}