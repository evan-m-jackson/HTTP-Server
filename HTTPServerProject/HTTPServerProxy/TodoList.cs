using HTTPServerRead.Streams;
using HTTPServerRead.Body;
using HTTPServerRead.Header;
using HTTPServerWrite.Streams;
using HTTPServerWrite.Request;

namespace HTTPServerProxy.TodoList;

public class TodoList
{
    IReadStreams _reader;
    IWriteStreams _writer;
    Header _header;
    Body _body;

    public TodoList(IReadStreams reader, IWriteStreams writer)
    {
        _reader = reader;
        _writer = writer;
        _header = new Header(_reader);
        _body = new Body(_reader);
    }
    
    public string GetString()
    {
        var body = "";
        while (body.Length == 0)
        {
            WriteToDoListRequest();
            body = GetToDoList();    
        }

        return body;
    }
    public void WriteToDoListRequest()
    {
        var initialLine = $"GET /todo-ids HTTP/1.1";
        var todoListRequest = new WriteRequest(_writer, initialLine);
        todoListRequest.Run();
    }
    
    public string GetToDoList()
    {
        var initialLine = _header.GetLine();
        var headers = _header.GetHeaders();
        var todoList = _body.GetBody();
        return todoList;
    }
}