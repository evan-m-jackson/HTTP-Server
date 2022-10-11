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

        bool socketIsOn = socket.socketStarted;

        Assert.False(socketIsOn);
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
    public bool socketStarted = new bool();

    public TestSocket(object client)
    {
        object test_client = client;
    }

    public void Start(){
        socketStarted = true;
    }

    public void Stop(){
        socketStarted = false;
    }

}