using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text.Json;
using HTTPServerWrite.Response;
using HTTPServerWrite.Streams;

namespace HTTPServerResponse.Path;

    public class ResponsePath
    {
        IWriteStreams writer;

        Dictionary<string, Dictionary<string, Dictionary<string, object>>> pathDict = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();
        public ResponsePath(IWriteStreams w, Dictionary<string, Dictionary<string, Dictionary<string, object>>> dict = default!)
        {
            writer = w;
            pathDict = dict;
        }

        public void ExecuteRequest(string path, string type, string requestBody)
        {
            if (pathDict.ContainsKey(path))
            {
                if (pathDict[path].ContainsKey(type))
                {
                Dictionary<string, object> paramDict = pathDict[path][type];
                int code = (int)paramDict["Status Code"];
                List<string> headers = (List<string>)paramDict["Headers"];
                string body = (string)paramDict["Body"];

                var response = new WriteResponse(writer, code, body, headers);
                response.GetResponse();
                }
                else
                {
                    var response = new WriteResponse(writer, 405, "");
                    response.GetResponse();
                }
            }

            else if (path == "echo_body")
            {
                var response = new WriteResponse(writer, 200, requestBody);
                response.GetResponse();
            }

            else if (path == "")
            {
                var response = new WriteResponse(writer, 200);
                response.GetResponse();
            }
            else
            {
                var response = new WriteResponse(writer, 404);
                response.GetResponse();
            }
        }

    }