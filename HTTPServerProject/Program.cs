using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HTTPServerProject.ReadStream;
using HTTPServerProject.RequestHeaders;
using HTTPServerProject.RequestBody;
using HTTPServerProject.Responses;
using HTTPServerProject.WriteStream;
using HTTPServerProject.Path;
using HTTPServerProject.Parameters;

namespace HTTPServerProject
{
    public class Server
    {

        TcpClient client;

        public Server(TcpClient tcpc)
        {
            client = tcpc;
        }

        public void Conversation()
        {
            var stream = client.GetStream();
            var writer = new WriteStreams(stream);
            var reader = new ReadStreams(stream);

            Console.WriteLine("Connection accepted.");

            var header = new Header(reader);
            var initialLine = header.GetLine();
            var rHeader = header.GetHeaders();

            var requestBody = new Body(reader);
            var bodyString = requestBody.GetBody();

            var httpType = header.GetRequestType(initialLine);
            var httpPath = header.GetPath(initialLine);

            var pathParams = new PathParameters();
            var pathDict = pathParams.pathDict;

            var execute = new RequestPath(writer, pathDict);
            execute.ExecuteRequest(httpPath, httpType, bodyString);

            Console.WriteLine("Message received: " + bodyString);
            Console.WriteLine("Message sent back: " + bodyString.GetType());

            Console.WriteLine("Closing the connection.");

            reader.Close();
            writer.Close();
            client.Close();
        }

        public static void Main(string[] args)
        {
            var port = 5000;
            var listener = new TcpListener(IPAddress.Any, port);

            try
            {

                listener.Start();

                Console.WriteLine("Server running on port {0}", port);

                while (true)
                {
                    var server = new Server(listener.AcceptTcpClient());

                    var serverThread = new Thread(new ThreadStart(server.Conversation));

                    serverThread.Start();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e + " " + e.StackTrace);
            }
            finally
            {
                listener.Stop();
            }

        }
    }
}