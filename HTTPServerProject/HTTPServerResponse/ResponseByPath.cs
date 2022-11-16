using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HTTPServerProject.Responses;
using HTTPServerProject.Responses.Write;
using HTTPServerProject.WriteStream;

namespace HTTPServerProject.Path
{
    public class RequestPath
    {
        IWriteStreams writer;

        Dictionary<string, Dictionary<string, Dictionary<string, object>>> pathDict = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();
        public RequestPath(IWriteStreams w, Dictionary<string, Dictionary<string, Dictionary<string, object>>> dict = default!)
        {
            writer = w;
            pathDict = dict;
        }

        public void ExecuteRequest(string path, string type, string requestBody)
        {
            if (pathDict.ContainsKey(path))
            {
                Dictionary<string, object> paramDict = pathDict[path][type];
                int code = (int)paramDict["Status Code"];
                List<string> headers = (List<string>)paramDict["Headers"];
                string body = (string)paramDict["Body"];

                var response = new WriteResponse(writer, code, body, headers);
                response.GetResponse();
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
}