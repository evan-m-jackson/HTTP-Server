namespace HTTPServerProject.Parameters;

public interface IPathParameters
{
    Dictionary<string, Dictionary<string, object>> GetTypeDict(string type, int code, List<string> headers, string body);
    Dictionary<string, object> GetParamDict(int code, List<string> headers, string body);
}
public class PathParameters : IPathParameters
{
    public Dictionary<string, Dictionary<string, Dictionary<string, object>>> pathDict = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();
    
    public PathParameters()
    {
        List<string> redirectHeaders = new List<string>(){"Location: http://127.0.0.1:5000/simple_get"};
        pathDict.Add("redirect", GetTypeDict("GET", 301, redirectHeaders, ""));

        List<string> mOHeaders = new List<string>(){"Allow: GET, HEAD, OPTIONS"};
        pathDict.Add("method_options", GetTypeDict("OPTIONS", 200, mOHeaders, ""));

        List<string> mOHeaders2 = new List<string>(){"Allow: GET, HEAD, OPTIONS, PUT, POST"};
        pathDict.Add("method_options2", GetTypeDict("OPTIONS", 200, mOHeaders2, ""));

        List<string> headHeaders = new List<string>(){"Allow: HEAD, OPTIONS"};
        pathDict.Add("head_request", GetTypeDict("HEAD", 200, headHeaders, ""));

        pathDict["head_request"].Add("GET", GetParamDict(405, headHeaders, ""));

        List<string> noHeaders = new List<string>();
        pathDict.Add("simple_get", GetTypeDict("GET", 200, noHeaders, ""));
        pathDict["simple_get"].Add("HEAD", GetParamDict(200, noHeaders, ""));
        pathDict.Add("simple_get_with_body", GetTypeDict("GET", 200, noHeaders, "Hello world"));

        List<string> textResponseHeaders = new List<string>(){"Content-Type: text/plain;charset=utf-8"};
        pathDict.Add("text_response", GetTypeDict("GET", 200, textResponseHeaders, "text response"));

        List<string> htmlResponseHeaders = new List<string>(){"Content-Type: text/html;charset=utf-8"};
        pathDict.Add("html_response", GetTypeDict("GET", 200, htmlResponseHeaders, "<html><body><p>HTML Response</p></body></html>"));

        List<string> jsonResponseHeaders = new List<string>(){"Content-Type: application/json;charset=utf-8"};
        pathDict.Add("json_response", GetTypeDict("GET", 200, jsonResponseHeaders, "{\"key1\":\"value1\",\"key2\":\"value2\"}"));

        List<string> xmlResponseHeaders = new List<string>(){"Content-Type: application/xml;charset=utf-8"};
        pathDict.Add("xml_response", GetTypeDict("GET", 200, xmlResponseHeaders, "<note><body>XML Response</body></note>"));

        List<string> kittehHeaders = new List<string>(){"Content-Type: image/jpeg"};
        pathDict.Add("kitteh.jpg", GetTypeDict("GET", 200, kittehHeaders, KittehBody()));

        List<string> doggoHeaders = new List<string>(){"Content-Type: image/png"};
        pathDict.Add("doggo.png", GetTypeDict("GET", 200, doggoHeaders, DoggoBody()));

        List<string> kissesHeaders = new List<string>(){"Content-Type: image/gif"};
        pathDict.Add("kisses.gif", GetTypeDict("GET", 200, kissesHeaders, KissesBody()));

        List<string> htmlCheckHeaders = new List<string>(){"Content-Type: text/html;charset=utf-8"};
        pathDict.Add("health-check.html", GetTypeDict("GET", 200, htmlCheckHeaders, HealthCheckBody()));
    }

    public Dictionary<string, Dictionary<string, object>> GetTypeDict(string type, int code, List<string> headers, string body)
    {
        Dictionary<string, object> paramDict = GetParamDict(code, headers, body);
        Dictionary<string, Dictionary<string, object>> typeDict = new Dictionary<string, Dictionary<string, object>>();
        typeDict.Add(type, paramDict);

        return typeDict;
    }

    public Dictionary<string, object> GetParamDict(int code, List<string> headers, string body)
    {
        Dictionary<string, object> paramDict = new Dictionary<string, object>();
        paramDict.Add("Status Code", code);
        paramDict.Add("Headers", headers);
        paramDict.Add("Body", body);

        return paramDict;
    }

    public string KittehBody()
    {
        return GetImageString("HTTPServerProject/web/kitteh.jpg");
    }

    public string DoggoBody()
    {
        return GetImageString("HTTPServerProject/web/doggo.png");
    }

    public string KissesBody()
    {
        return GetImageString("HTTPServerProject/web/kisses.gif");
    }

    public string HealthCheckBody()
    {
        return File.ReadAllText("HTTPServerProject/web/health-check.html");
    }

    public string GetImageString(string filePath)
    {
        byte[] imageArray = File.ReadAllBytes(filePath);
        string imageString = Convert.ToBase64String(imageArray);
        return imageString;
    }
}