using HTTPServerReadTests.Streams;
using HTTPServerProxy.Response;
using HTTPServerWriteTests.Streams;
using HTTPServerProxy.FirstID;

namespace HTTPServerProxyTests.FirstIDTest;

public class UnitTestsForFirstID
{

    [Fact]
    public void GetTodoListTest()
    {
        var proxyStream = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: application/json", "", "[{'id':1}]" };
        var reader = new TestReadStreams(proxyStream);
        var writer = new TestWriteStreams(proxyStream);
        var httpType = "PUT";
        var httpPath = "todo/1";
        
        var firstID = new FirstID(reader, writer, httpPath, httpType);
        var todoList = firstID.GetToDoList();
    
        Assert.Equal("[{'id':1}]", todoList);
    }
    
    [Fact]
    public void GetTodoListIsEmptyTest()
    {
        var proxyStream = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: application/json", "", "[]" };
        var reader = new TestReadStreams(proxyStream);
        var writer = new TestWriteStreams(proxyStream);
        var httpType = "PUT";
        var httpPath = "todo/1";

        var firstID = new FirstID(reader, writer, httpPath, httpType);
        var todoList = firstID.GetToDoList();
        var actual = firstID.ListEmpty(todoList);

        Assert.True(actual);
    }

    [Fact]
    public void GetTodoListHasTodosTest()
    {
        var proxyStream = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: application/json", "", "[{'id':1}]" };
        var reader = new TestReadStreams(proxyStream);
        var writer = new TestWriteStreams(proxyStream);
        var httpType = "PUT";
        var httpPath = "todo/1";
        
        var firstID = new FirstID(reader, writer, httpPath, httpType);
        var todoList = firstID.GetToDoList();
        var actual = firstID.ListEmpty(todoList);
    
        Assert.False(actual);
    }

    [Fact]
    public void GetTodoListFirstIDTest()
    {
        var proxyStream = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: application/json", "", "[{'id':1}]" };
        var reader = new TestReadStreams(proxyStream);
        var writer = new TestWriteStreams(proxyStream);
        var httpType = "PUT";
        var httpPath = "todo/1";
        
        var firstID = new FirstID(reader, writer, httpPath, httpType);
        var todoList = firstID.GetToDoList();
        var iD = firstID.GetID(todoList);
    
        Assert.Equal("1", iD);
    }
}