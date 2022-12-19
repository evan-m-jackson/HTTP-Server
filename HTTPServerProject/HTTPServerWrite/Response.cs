using HTTPServerWrite.Streams;
using HTTPServerResponse.StatusCode;

namespace HTTPServerWrite.Response;

    public class WriteResponse
    {
        IWriteStreams _writer;
        public List<string> _headers = new List<string>();
        int _code;
        string _body;

        public WriteResponse(IWriteStreams writer, int code, string body = "", List<string> headers = default!)
        {
            _writer = writer;
            _code = code;
            _body = body;
            _headers = headers;
        }
        public void GetResponse()
        {
            WriteResponseStatusCode();
            WriteResponseHeaders();
            _writer.WriteLine();
            WriteResponseBody();
        }

        public void WriteResponseHeaders()
        {
            if (_headers != default)
            {
                foreach (var name in _headers)
                {
                    _writer.WriteLine($"{name}");
                }
            }
        }

        public void AddHeader(string name, string description = default!)
        {
            _headers.Add(name);
        }

        public void WriteResponseBody()
        {
            if (_body.Length > 0)
            {
                _writer.Write(_body);
                _writer.Flush();
            }
            else
            {
                _writer.Flush();
            }

        }

        public void WriteResponseStatusCode()
        {
            var sc = new ResponseCode(_code);
            var message = sc.GetStatus();
            _writer.WriteLine(message);
        }
    }