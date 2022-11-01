using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HTTPServerProject.ReadStreams;
using HTTPServerProject.Headers;
using HTTPServerProject.Request.Body;
using HTTPServerProject.WriteStreams;

namespace HTTPServerProject.Tests;

public class IntegrationTestForServer
{

    [Fact]
    public void TestStartupConnectAndShutdown()
    {

        Thread serverThread = new Thread(new ThreadStart(RunServer));

        string expected = "What's up?";
        string result = string.Empty;

        Thread clientThread = new Thread(() => { result = RunClient(expected); });

        clientThread.Start();
        serverThread.Start();
        clientThread.Join();

        Assert.Equal(expected, result);

    }

    private static void RunServer()
    {
        Server.Main(Array.Empty<String>());
    }

    public static string RunClient(string input)
    {
        Console.WriteLine("Starting client...");

        TcpClient client = new TcpClient("127.0.0.1", 5000);
        NetworkStream stream = client.GetStream();
        StreamReader reader = new StreamReader(stream);
        StreamWriter writer = new StreamWriter(stream);

        writer.WriteLine(input);
        writer.Flush();

        return input;
    }


}

public class UnitTestsForConversation
{
    public List<string> request = new List<string>() { "GET / HTTP/1.1", "Host: localhost:5000", "User-Agent: curl/7.79.1", "Accept: */*", "", "quit" };

    [Fact]
    public void GetInitialLineTest()
    {
        var expected = "GET / HTTP/1.1";
        var reader = new TestStreamReader(request);

        Header header = new Header(reader);
        var initialLine = header.GetLine();

        Assert.Equal(initialLine, expected);

    }

    [Fact]
    public void GetRequestHeadersTest()
    {
        var expected = new List<string>() { "Host: localhost:5000", "User-Agent: curl/7.79.1", "Accept: */*" };
        var reader = new TestStreamReader(request);

        Header header = new Header(reader);
        var initialLine = header.GetLine();
        var headers = header.GetHeaders();

        Assert.Equal(headers, expected);

    }

    [Fact]
    public void GetRequestBodyTest()
    {
        var reader = new TestStreamReader(request);

        Header header = new Header(reader);
        var initialLine = header.GetLine();
        var headers = header.GetHeaders();

        Body body = new Body(reader);
        string expected = body.GetBody();

        Assert.Equal("quit", expected);
    }

    [Fact]
   public void GiveResponseHeaderTest()
   {
        var eStream = new List<string>();
        var expected = new List<string>() {"HTTP/1.1 200 OK", ""};
        var writer = new TestStreamWriter(eStream);

        ResponseHeader resHeader = new ResponseHeader(writer);
        resHeader.GiveResponseHeader();

        Assert.Equal(eStream, expected); 
   }

}


