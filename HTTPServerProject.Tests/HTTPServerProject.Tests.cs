using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace HTTPServerProject.Tests;

public class IntegrationTestForServer
{

    [Fact]
    public void TestStartupConnectAndShutdown()
    {

        Thread serverThread = new Thread(new ThreadStart(RunServer));
        Thread clientThread = new Thread(new ThreadStart(RunClient));

        serverThread.Start();
        clientThread.Start();


    }

    private static void RunServer()
    {
        Server.Main(Array.Empty<String>());
    }

    private static void RunClient()
    {
        Console.WriteLine("Starting client...");

        TcpClient client = new TcpClient("127.0.0.1", 5000);
        NetworkStream stream = client.GetStream();
        StreamReader reader = new StreamReader(stream);
        StreamWriter writer = new StreamWriter(stream);


        String input = "Hello World!";

        while (input != "quit")
        {
            writer.WriteLine(input);
            writer.Flush();

            input = "quit";
        }

        writer.WriteLine("quit");
        writer.Flush();
    }

}

