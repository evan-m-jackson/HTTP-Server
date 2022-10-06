using System.ComponentModel;
using System;
using System.Net;
using System.Net.Sockets;

namespace sockets
{
    class Program
    {
        public static void Main(string[] args)
        { 
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);

            listener.Start();

            Console.WriteLine("Waiting for Client...");
            while (true)
            {
                Socket socket = listener.AcceptSocket();
                NetworkStream stream = new NetworkStream(socket);
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                string text = reader.ReadLine();
                
                writer.WriteLine("Hello World!");
                stream.Close();
                socket.Close();
            }

        }
    }
}