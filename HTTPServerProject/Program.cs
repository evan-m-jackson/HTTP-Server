using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HTTPServerProject.Interfaces;
using HTTPServerProject.Headers;

namespace HTTPServerProject
{
    public class Server
    {

        TcpClient client = null!;
        NetworkStream stream = default!;
        NewStreamReader reader = default!;
        StreamWriter writer = default!;

        public Server(TcpClient tcpc)
        {
            client = tcpc;
            stream = client.GetStream();
            reader = new MyStreamReader(stream);
            writer = new StreamWriter(stream);
        }

        public void Conversation()
        {
            Console.WriteLine("Connection accepted.");

            Header header = new Header(reader);
            var initialLine = header.GetLine();
            var rHeader = header.GetHeaders();

            var input = reader.ReadLine()!;

            while (input != null)
            {
                Console.WriteLine("Message received: " + input);
                writer.WriteLine(input);
                writer.Flush();
                Console.WriteLine("Message sent back: " + input);
                input = reader.ReadLine()!;
            }

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