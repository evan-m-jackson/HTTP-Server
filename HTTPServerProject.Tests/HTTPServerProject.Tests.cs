using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HTTPServerProject.ReadStream;
using HTTPServerProject.Request.Headers;
using HTTPServerProject.Request.Body;
using HTTPServerProject.WriteStream;
using HTTPServerProject.Responses;

namespace HTTPServerProject.Tests;

public class IntegrationTestForServer
{

    [Fact]
    public void TestStartupConnectAndShutdown()
    {

        var serverThread = new Thread(new ThreadStart(RunServer));

        var expected = "What's up?";
        var result = string.Empty;

        var clientThread = new Thread(() => { result = RunClient(expected); });

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

        var client = new TcpClient("127.0.0.1", 3000);
        var stream = client.GetStream();
        var reader = new StreamReader(stream);
        var writer = new StreamWriter(stream);

        writer.WriteLine(input);
        writer.Flush();

        return input;
    }


}

public class UnitTestsForRequests
{
    public List<string> request = new List<string>() { "GET / HTTP/1.1", "Host: localhost:5000", "User-Agent: curl/7.79.1", "Accept: */*", "", "quit" };

    [Fact]
    public void GetRequestInitialLineTest()
    {
        var expected = "GET / HTTP/1.1";
        var reader = new TestReadStreams(request);

        var header = new Header(reader);
        var initialLine = header.GetLine();

        Assert.Equal(initialLine, expected);

    }

    [Fact]
    public void GetRequestHeadersTest()
    {
        var expected = new List<string>() { "Host: localhost:5000", "User-Agent: curl/7.79.1", "Accept: */*" };
        var reader = new TestReadStreams(request);

        var header = new Header(reader);
        var initialLine = header.GetLine();
        var headers = header.GetHeaders();

        Assert.Equal(headers, expected);

    }

    [Fact]
    public void GetRequestBodyTest()
    {
        var reader = new TestReadStreams(request);

        var header = new Header(reader);
        var initialLine = header.GetLine();
        var headers = header.GetHeaders();

        var body = new Body(reader);
        var expected = body.GetBody();

        Assert.Equal("quit", expected);
    }


}

public class UnitTestsForResponses
{
    public List<string> eStream = new List<string>();

    [Fact]
   public void GiveResponseHeaderTest()
   {
        var expected = new List<string>() {"HTTP/1.1 200 OK", ""};
        var writer = new TestWriteStreams(eStream);

        var response = new Response(writer);
        response.WriteResponseHeader();

        Assert.Equal(eStream, expected); 
   }

   [Fact]
   public void GiveResponseBodyTest()
   {
        var expected = new List<string>() {"HTTP/1.1 200 OK", "", "Hello World!"};
        var writer = new TestWriteStreams(eStream);
        var input = "Hello World!";

        var response = new Response(writer);
        response.WriteResponse(input);

        Assert.Equal(eStream, expected);
   }
}


