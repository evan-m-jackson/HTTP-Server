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
        
        if (_type == "POST")
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
        
        else if (_type == "PUT")
        {
            if (code == 200)
            {
                GetFullJSONContentType(headers);
                Console.WriteLine(body);
                if (body.StartsWith('{') && body.EndsWith('}') && body.Length > 2)
                {
                    var response = new WriteResponse(_writer, 200, body, headers);
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
        
        else if (_type == "DELETE")
        {
            var response = new WriteResponse(_writer, 204);
            response.GetResponse();
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