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

        public Dictionary<string, object> pathDict = new Dictionary<string, object>();
        public RequestPath(IWriteStreams w)
        {
            writer = w;
        }

        public void ExecuteRequest(string path, string type, string requestBody)
        {
            if (path == "redirect")
            {
                var response = new WriteResponse(writer, 301);
                response.AddHeader("Location", "http://127.0.0.1:5000/simple_get");
                response.GetResponse();
            }

            else if (path == "method_options")
            {
                var response = new WriteResponse(writer, 200);
                response.AddHeader("Allow", "GET, HEAD, OPTIONS");
                response.GetResponse();
            }

            else if (path == "method_options2")
            {
                var response = new WriteResponse(writer, 200);
                response.AddHeader("Allow", "GET, HEAD, OPTIONS, PUT, POST");
                response.GetResponse();
            }

            else if (path == "head_request")
            {
                if (type == "HEAD")
                {
                    var response = new WriteResponse(writer, 200);
                    response.AddHeader("Allow", "HEAD, OPTIONS");
                    response.GetResponse();
                }
                else
                {
                    var response = new WriteResponse(writer, 405);
                    response.AddHeader("Allow", "HEAD, OPTIONS");
                    response.GetResponse();
                }

            }

            else if (path == "simple_get")
            {
                var response = new WriteResponse(writer, 200);
                response.GetResponse();
            }

            else if (path == "simple_get_with_body")
            {
                var response = new WriteResponse(writer, 200, "Hello world");
                response.GetResponse();
            }

            else if (path == "echo_body")
            {
                var response = new WriteResponse(writer, 200, requestBody);
                response.GetResponse();
            }

            else if (path == "text_response")
            {
                var response = new WriteResponse(writer, 200, "text response");
                response.AddHeader("Content-Type", "text/plain;charset=utf-8");
                response.GetResponse();
            }

            else if (path == "html_response")
            {
                var response = new WriteResponse(writer, 200, "<html><body><p>HTML Response</p></body></html>");
                response.AddHeader("Content-Type", "text/html;charset=utf-8");
                response.GetResponse();
            }

            else if (path == "json_response")
            {
                var response = new WriteResponse(writer, 200, "{\"key1\":\"value1\",\"key2\":\"value2\"}");
                response.AddHeader("Content-Type", "application/json;charset=utf-8");
                response.GetResponse();
            }

            else if (path == "xml_response")
            {
                var response = new WriteResponse(writer, 200, "<note><body>XML Response</body></note>");
                response.AddHeader("Content-Type", "application/xml;charset=utf-8");
                response.GetResponse();
            }

            else if (path == "kitteh.jpg")
            {
                byte[] imageArray = File.ReadAllBytes("HTTPServerProject/HTTPServerResponse/web/kitteh.jpg");
                string imageString = Convert.ToBase64String(imageArray);
                var response = new WriteResponse(writer, 200, imageString);
                response.AddHeader("Content-Type", "image/jpeg");
                response.GetResponse();
            }

            else if (path == "doggo.png")
            {
                byte[] imageArray = File.ReadAllBytes("HTTPServerProject/HTTPServerResponse/web/doggo.png");
                string imageString = Convert.ToBase64String(imageArray);
                var response = new WriteResponse(writer, 200, imageString);
                response.AddHeader("Content-Type", "image/png");
                response.GetResponse();
            }

            else if (path == "kisses.gif")
            {
                byte[] imageArray = File.ReadAllBytes("HTTPServerProject/HTTPServerResponse/web/kisses.gif");
                string imageString = Convert.ToBase64String(imageArray);
                var response = new WriteResponse(writer, 200, imageString);
                response.AddHeader("Content-Type", "image/gif");
                response.GetResponse();
            }

            else if (path == "health-check.html")
            {
                var htmlText = File.ReadAllText("HTTPServerProject/HTTPServerResponse/web/health-check.html");
                var response = new WriteResponse(writer, 200, htmlText);
                response.AddHeader("Content-Type", "text/html;charset=utf-8");
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