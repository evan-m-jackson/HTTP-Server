namespace HTTPServerProject.Responses.Code;

public class ResponseCode
{
    string codeString;
    Dictionary<string, string> statusMessages = new Dictionary<string, string>();

    public ResponseCode(int c)
    {
        codeString = c.ToString();
        statusMessages.Add("200", "OK");
        statusMessages.Add("201", "Created");
        statusMessages.Add("204", "No Content");
        statusMessages.Add("301", "Moved Permanently");
        statusMessages.Add("400", "Bad Request");
        statusMessages.Add("404", "Not Found");
        statusMessages.Add("405", "Method Not Allowed");
        statusMessages.Add("415", "Unsupported Media Type");
        statusMessages.Add("500", "Internal Server Error");
    }

    public string GetStatus()
    {
        return $"HTTP/1.1 {codeString} {statusMessages[codeString]}";
    }
}