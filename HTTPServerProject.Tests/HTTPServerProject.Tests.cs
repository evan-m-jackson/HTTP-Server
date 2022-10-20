using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HTTPServerProject.Interfaces;

namespace HTTPServerProject.Tests;

public class IntegrationTestForServer
{

    [Fact]
    public void TestStartupConnectAndShutdown()
    {

        Thread serverThread = new Thread(new ThreadStart(RunServer));

        string expected = "Hello World";
        string result = null!;

        Thread clientThread = new Thread(() => { result = RunClient(expected); });

        serverThread.Start();
        clientThread.Start();

        clientThread.Join();

        Assert.Equal(expected, result);
    }

    private static void RunServer()
    {
        Server.Main(Array.Empty<String>());
    }

    private static string RunClient(string input)
    {
        Console.WriteLine("Starting client...");

        TcpClient client = new TcpClient("127.0.0.1", 5000);
        NetworkStream stream = client.GetStream();
        StreamReader reader = new StreamReader(stream);
        StreamWriter writer = new StreamWriter(stream);

        writer.WriteLine(input);
        writer.Flush();

        string result = reader.ReadLine()!;

        return result;
    }


}

public class UnitTestsForConversation
{
    public List<string> request = new List<string>(){"GET / HTTP/1.1", "Host: localhost:5000", "User-Agent: curl/7.79.1", "Accept: */*", null!, "quit"};

    [Fact]
    public void GetInitialLineTest()
    {
        var expected = "GET / HTTP/1.1";
        TestStreamReader reader = new TestStreamReader(request);
        var input = reader.ReadLine();
        
        Assert.Equal(input, expected);

    }

}


