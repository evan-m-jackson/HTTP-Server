using HTTPServerProject.WriteStream;
using HTTPServerProject.Responses.Code;

namespace HTTPServerProject.Responses.Write
{
    public class WriteResponse
    {
        IWriteStreams streamWriter;
        public Dictionary<string, string> headers = new Dictionary<string, string>();
        int statusCode;

        string resBody;

        public WriteResponse(IWriteStreams writer, int code, string body = "")
        {
            streamWriter = writer;
            statusCode = code;
            resBody = body;
        }
        public void GetResponse()
        {
            WriteResponseStatusCode();
            WriteResponseHeaders();
            streamWriter.WriteLine();
            WriteResponseBody();
        }

        public void WriteResponseHeaders()
        {
            foreach (var (name, description) in headers)
            {
                streamWriter.WriteLine($"{name}: {description}");
            }
        }

        public void AddHeader(string name, string description)
        {
            headers.Add(name, description);
        }

        public void WriteResponseBody()
        {
            if (resBody.Length > 0)
            {
                streamWriter.Write(resBody);
                streamWriter.Flush();
            }
            else
            {
                streamWriter.Flush();
            }

        }

        public void WriteResponseStatusCode()
        {
            var sc = new ResponseCode(statusCode);
            var message = sc.GetStatus();
            streamWriter.WriteLine(message);
        }

    }
}