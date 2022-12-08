using HTTPServerWrite.Streams;

namespace HTTPServerWrite.Request;

public class WriteRequest
{
    IWriteStreams _writer;
    string _initialLine;
    List<string> _headers;
    string _body;
    
    public WriteRequest(IWriteStreams writer, string initialLine, List<string> headers = default!, string body = "")
    {
        _writer = writer;
        _initialLine = initialLine;
        _headers = headers;
        _body = body;
    }

    public void GetRequest()
    {
        _writer.WriteLine(_initialLine);
        WriteRequestHeaders();
        WriteRequestBody();
    }

    public void WriteRequestHeaders()
    {
        if (_headers != default)
        {
            foreach(var header in _headers)
            {
                
                _writer.WriteLine(header);
            }   
        }
        _writer.WriteLine();
    }

    public void WriteRequestBody()
    {
        if (_body.Length > 0)
        {
            _writer.Write(_body);
        }
        _writer.Flush();
    }
}