using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace HTTPServerProject.Tests;

public class HTTPServerProjectTests
{
    [Fact]
    public void SocketTest()
    {
        object listener = new object();

        TestSocket socket = new TestSocket(listener);

        socket.Start();

        socket.Stop();

        bool listenerIsOn = socket.listenerStarted;

        Assert.False(listenerIsOn);
    }
}

public class TestServer
{
    public TestServer(object client)
    {

    }
}

public class TestConversation
{
    public TestConversation(object server)
    {
        object test_server = server;
    }

}

public class TestSocket
{
    public bool listenerStarted = new bool();

    public TestSocket(object client)
    {
        object listener = client;
    }

    public void Start()
    {
        listenerStarted = true;
    }

    public void Stop()
    {
        listenerStarted = false;
    }

}