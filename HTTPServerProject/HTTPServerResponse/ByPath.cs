using HTTPServerWrite.Response;
using HTTPServerWrite.Streams;

namespace HTTPServerResponse.Path;

    public class ResponsePath
    {
        IWriteStreams _writer;

        Dictionary<string, Dictionary<string, Dictionary<string, object>>> _dict = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();
        public ResponsePath(IWriteStreams writer, Dictionary<string, Dictionary<string, Dictionary<string, object>>> dict)
        {
            _writer = writer;
            _dict = dict;
        }

        public void Execute(string path, string type, string requestBody)
        {
            if (_dict.ContainsKey(path))
            {
                if (_dict[path].ContainsKey(type))
                {
                Dictionary<string, object> paramDict = _dict[path][type];
                int code = (int)paramDict["Status Code"];
                List<string> headers = (List<string>)paramDict["Headers"];
                string body = (string)paramDict["Body"];

                var response = new WriteResponse(writer: _writer, code: code, body: body, headers: headers);
                response.Run();
                }
                else
                {
                    var response = new WriteResponse(writer: _writer, code: 405, body: "");
                    response.Run();
                }
            }

            else if (path == "echo_body")
            {
                var response = new WriteResponse(writer: _writer, code: 200, body: requestBody);
                response.Run();
            }

            else if (path == "")
            {
                var response = new WriteResponse(_writer, 200);
                response.Run();
            }
            else
            {
                var response = new WriteResponse(_writer, 404);
                response.Run();
            }
        }

    }