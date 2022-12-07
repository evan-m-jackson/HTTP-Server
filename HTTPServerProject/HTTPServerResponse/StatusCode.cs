namespace HTTPServerResponse.StatusCode;

public class ResponseCode
{
    string _codeString;
    Dictionary<string, string> _statusMessages = new Dictionary<string, string>();

    public ResponseCode(int c)
    {
        _codeString = c.ToString();
        _statusMessages.Add("200", "OK");
        _statusMessages.Add("201", "Created");
        _statusMessages.Add("204", "No Content");
        _statusMessages.Add("301", "Moved Permanently");
        _statusMessages.Add("400", "Bad Request");
        _statusMessages.Add("404", "Not Found");
        _statusMessages.Add("405", "Method Not Allowed");
        _statusMessages.Add("415", "Unsupported Media Type");
        _statusMessages.Add("500", "Internal Server Error");
    }

    public string GetStatus()
    {
        return $"HTTP/1.1 {_codeString} {_statusMessages[_codeString]}";
    }
}