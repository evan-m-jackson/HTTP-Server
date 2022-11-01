using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HTTPServerProject.ReadStreams;
using HTTPServerProject.Request.Headers;
using HTTPServerProject.Request.Body;
using HTTPServerProject.Responses;
using HTTPServerProject.WriteStreams;

namespace HTTPServerProject
{
    public class Server
    {

        TcpClient client = null!;

        public Server(TcpClient tcpc)
        {
            client = tcpc;
        }

        public void Conversation()
        {
            NetworkStream stream = client.GetStream();
            MyStreamWriter writer = new MyStreamWriter(stream);
            MyStreamReader reader = new MyStreamReader(stream);

            Console.WriteLine("Connection accepted.");

            Header header = new Header(reader);
            var initialLine = header.GetLine();
            var rHeader = header.GetHeaders();

            Body body = new Body(reader);
            var input = body.GetBody();

            Response response = new Response(writer);
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
            int port = 5000;
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);

            try
            {
                
                listener.Start();

                Console.WriteLine("Server running on port {0}", port);

                while (true)
                {
                    Server server = new Server(listener.AcceptTcpClient());

                    Thread serverThread = new Thread(new ThreadStart(server.Conversation));

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