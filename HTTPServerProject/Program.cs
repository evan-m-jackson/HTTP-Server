using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HTTPServerProject.ReadStream;
using HTTPServerProject.Request.Headers;
using HTTPServerProject.Request.Body;
using HTTPServerProject.Responses;
using HTTPServerProject.WriteStream;

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

            var body = new Body(reader);
            var input = body.GetBody();

            var response = new Response(writer);
            response.WriteResponse(input);

            Console.WriteLine("Message received: " + input);
            Console.WriteLine("Message sent back: " + input);

            Console.WriteLine("Closing the connection.");

            reader.Close();
            writer.Close();
            client.Close();
        }

        public static void Main(string[] args)
        {
            var port = 3000;
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