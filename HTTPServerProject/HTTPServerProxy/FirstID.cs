using HTTPServerRead.Streams;
using HTTPServerRead.Header;
using HTTPServerRead.Body;
using HTTPServerWrite.Request;
using HTTPServerWrite.Streams;

namespace HTTPServerProxy.FirstID;

public class FirstID
{
    IReadStreams _reader;
    IWriteStreams _writer;
    Header _header;
    Body _body;
    string _path;
    string _type;

    public FirstID(IReadStreams reader, IWriteStreams writer, string path, string type)
    {
        _reader = reader;
        _writer = writer;
        _header = new Header(_reader);
        _body = new Body(_reader);
        _path = path;
        _type = type;
    }

	public string GetFirstIndex()
    {
        var body = RunToListRequest();
        if (ListEmpty(body))
        {
            return "1";
        }
        var result = GetID(body);
        return result;
	}
    public string GetID(string body)
    {
        var foundInt = false;
        var result = "";
        foreach (char c in body)
        {
            if (Char.IsDigit(c) && !foundInt)
            {
                foundInt = true;
                result += c;
            }
            else if (Char.IsDigit(c))
            {
                result += c;
            }
            else if (foundInt && !Char.IsDigit(c))
            {
                break;
            }
        }
        return result;
    }

	public bool ListEmpty(string body)
    {
        var todoListLength = body.Length;
        return todoListLength == 2;
    }
    
    public string RunToListRequest()
    {
        var body = "";
        while (body.Length == 0)
        {
            WriteToDoListRequest();
            body = GetToDoList();    
        }

        return body;
    }
    
    public string GetToDoList()
    {
        var initialLine = _header.GetLine();
        var headers = _header.GetHeaders();
        var todoList = _body.GetBody();
        return todoList;
    }

    public void WriteToDoListRequest()
    {
        var initialLine = GetInitialLine("GET", "todo-ids");
        var todoListRequest = new WriteRequest(_writer, initialLine);
        todoListRequest.GetRequest();
    }
    
    public string GetInitialLine(string method, string path)
    {
        var initialLine = $"{method} /{path} HTTP/1.1";
        return initialLine;
    }
}