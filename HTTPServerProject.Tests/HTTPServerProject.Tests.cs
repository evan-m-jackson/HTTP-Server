using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace HTTPServerProject.Tests;

public class HTTPServerProjectTests
{
    [Fact]
    public void Test1()
    {
        TcpClient client = null;

        Server server = new Server(client);

        Assert.NotNull(server);
    }
}