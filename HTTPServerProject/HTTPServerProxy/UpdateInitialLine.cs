using HTTPServerRead.Streams;
using HTTPServerRead.Header;
using HTTPServerRead.Body;
using HTTPServerWrite.Request;
using HTTPServerWrite.Streams;
using HTTPServerProxy.FirstID;

namespace HTTPServerProject.Update.Initial.Line;

public class UpdateInitialLine
{
    string _path;
    string _type;
    GetFirstID _firstId;

    public UpdateInitialLine(string path, string type, GetFirstID firstId)
    {
        _path = path;
        _type = type;
        _firstId = firstId;
    }

    public string Run()
    {
        if (_path == "todo/1")
        {
            var id = _firstId.Run();
            _path = _path.Replace("todo/1", $"todo/{id}");
        }

        if (_type == "DELETE")
        {
            _path = _path.Replace("todo", "todo-delete");
        }

        var initialLine = GetInitialLine();
        return initialLine;
    }

    public string GetInitialLine()
    {
        var result = $"{_type} /{_path} HTTP/1.1";
        return result;
    }
}