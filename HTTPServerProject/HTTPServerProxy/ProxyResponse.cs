using HTTPServerProject.ReadStream;
using HTTPServerProject.ReadHeaders;
using HTTPServerProject.ReadBody;
using HTTPServerProject.Responses.Write;
using HTTPServerProject.WriteStream;

namespace HTTPServerProject.Proxy.Response;

public class ProxyResponse
{
    Header _header;
    Body _body;
    IReadStreams _reader;
    IWriteStreams _writer;
    string _path;
    string _type;

    public ProxyResponse(IReadStreams reader, IWriteStreams writer, string path, string type)
    {
        _reader = reader;
        _writer = writer;
        _header = new Header(_reader);
        _body = new Body(_reader);
        _path = path;
        _type = type;
    }

    public void GetResponse()
    {
        var code = GetStatusCode();
        var headers = GetHeaders();
        var body = GetBody();
        
        if (_path == "todo" && _type == "POST")
        {
            if (code == 200)
            {
                GetFullJSONContentType(headers);
                if (body.StartsWith('{') && body.EndsWith('}') && body.Length > 2)
                {
                    Console.WriteLine(body);
                    var response = new WriteResponse(_writer, 201, body, headers);
                    response.GetResponse();
                }
                else
                {
                    var response = new WriteResponse(_writer, 400);
                    response.GetResponse();
                }
            }
            else
            {
                var response = new WriteResponse(_writer, code);
                response.GetResponse();
            }
        }
    }
    
    private int GetStatusCode()
    {
        var proxyStatusLine = _header.GetLine();
        var proxyStatusCode = _header.GetCode(proxyStatusLine);
        return proxyStatusCode;
    }

    private List<string> GetHeaders()
    {
        var proxyHeaders = _header.GetHeaders();
        return proxyHeaders;
    }

    private string GetBody()
    {
        var input = _reader.Peek();
        var result = "";
        var addToResult = false;
        while ((input != -1))
        {
            var r = _reader.Read();
            var c = (char)r;

            if (c == ',')
            {
                addToResult = true;
            }
            else if (addToResult || c == '{') 
            {
                result += c;    
            }
            input = _reader.Peek();
        }
        return result;
    }

    private string GetTaskFromJSON(string body)
    {
        var jsonList = body.Split(',');
        return "{" + jsonList[1] + "}";
    }

    private void GetFullJSONContentType(List<string> headers)
    {
        var oldJSONHeader = "Content-Type: application/json";
        var newJSONHeader = "Content-Type: application/json;charset=utf-8";
        if (headers.Contains(oldJSONHeader))
        {
            var idx = headers.IndexOf(oldJSONHeader);
            headers[idx] = newJSONHeader;
        }
    }
}