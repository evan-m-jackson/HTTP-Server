using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

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
            Console.WriteLine("Connection accepted.");

            var stream = client.GetStream();
            var reader = new StreamReader(stream);
            var writer = new StreamWriter(stream);

            var input = reader.ReadLine()!;

            while (input != "quit")
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