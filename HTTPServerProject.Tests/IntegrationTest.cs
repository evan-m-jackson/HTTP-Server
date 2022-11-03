namespace HTTPServerProject.Tests.Integration;

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