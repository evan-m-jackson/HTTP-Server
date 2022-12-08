using HTTPServerRead.Streams;
using HTTPServerRead.Header;
using HTTPServerRead.Body;
using HTTPServerWrite.Response;
using HTTPServerWrite.Streams;

namespace HTTPServerProxy.Response;

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
        var status = StatusWrapper(_type);
        
        if (_type == "DELETE")
        {
            var response = new WriteResponse(_writer, status);
            response.GetResponse();
        }
        else
        {
            if (code == 200)
            {
                
                var contentLength = GetContentLength(headers);
                GetFullJSONContentType(headers);
                if (contentLength > 2)
                {
                    var response = new WriteResponse(_writer, status, body, headers);
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
        Console.WriteLine(proxyStatusLine);
        var proxyStatusCode = _header.GetCode(proxyStatusLine);
        Console.WriteLine(proxyStatusCode);
        return proxyStatusCode;
    }

    private int StatusWrapper(string type)
    {
        if (type == "POST")
        {
            return 201;
        }
        else if (type == "DELETE")
        {
            return 204;
        }
        else
        {
            return 200;
        }
    }

    private List<string> GetHeaders()
    {
        var proxyHeaders = _header.GetHeaders();
        return proxyHeaders;
    }

    public int GetContentLength(List<string> headers)
    {
        var str = "Content-Length: ";
        var length = str.Length;
        var result = "";
        foreach (var header in headers)
        {
            if (header.Contains("Content-Length:"))
            {
                result = header;
                break;
            }
        }

        result = result.Substring(length);
        return Int32.Parse(result);
    }

    private string GetBody()
    {
        var result = _body.GetBody();
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