using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace HTTPServerProject.Tests;

public class HTTPServerProjectTests
{
    [Fact]
    public void ServerTest()
    {
        object client = new object();

        TestServer server = new TestServer(client);

        Assert.NotNull(server);
    }

    [Fact]
    public void SocketTest()
    {
        object client = new object();

        TestSocket socket = new TestSocket(client);

        Assert.NotNull(socket);
    }
}



public class TestServer 
{
    public TestServer(object client)
    {

    }
}

public class TestSocket
{
    public TestSocket(object client)
    {
        
    }
}