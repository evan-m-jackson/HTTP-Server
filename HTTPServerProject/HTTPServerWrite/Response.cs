using HTTPServerWrite.Streams;
using HTTPServerResponse.StatusCode;

namespace HTTPServerWrite.Response;

    public class WriteResponse
    {
        IWriteStreams streamWriter;
        public List<string> resHeaders = new List<string>();
        int statusCode;

        string resBody;

        public WriteResponse(IWriteStreams writer, int code, string body = "", List<string> headers = default!)
        {
            streamWriter = writer;
            statusCode = code;
            resBody = body;
            resHeaders = headers;
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
            if (resHeaders != default)
            {
            foreach (var name in resHeaders)
            {
                streamWriter.WriteLine($"{name}");
            }
            }
        }

        public void AddHeader(string name, string description = default!)
        {
            resHeaders.Add(name);
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