using HTTPServerRead.Streams;
using HTTPServerRead.Header;
using HTTPServerRead.Body;
using HTTPServerWrite.Request;
using HTTPServerWrite.Streams;
using HTTPServerProxy.FirstID;

namespace HTTPServerProject.Update.Initial.Line;

public class UpdateInitialLine
{
    IReadStreams _reader;
    IWriteStreams _writer;
    Header _header;
    Body _body;
    string _path;
    string _type;
    IGetFirstID _firstId;

    public UpdateInitialLine(IReadStreams reader, IWriteStreams writer, string path, string type, IGetFirstID firstId)
    {
        _reader = reader;
        _writer = writer;
        _header = new Header(_reader);
        _body = new Body(_reader);
        _path = path;
        _type = type;
        _firstId = firstId;
    }

    public string Run()
    {
        if (_path == "todo/1")
        {
            var id = _firstId.GetFirstIndex();
            _path = _path.Replace("todo/1", $"todo/{id}");
        }

        if (_type == "DELETE")
        {
            _path = _path.Replace("todo", "todo-delete");
        }

        var initialLine = GetInitialLine(_type, _path);
        return initialLine;
    }

    public string GetInitialLine(string type, string path)
    {
        var result = _firstId.GetInitialLine(type, path);
        return result;
    }
}