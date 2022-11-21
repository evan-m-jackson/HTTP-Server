using System;
using System.Net;
using System.Net.Sockets;

namespace HTTPServerProject.Proxy.Client;

public interface IProxyClient
{
}

public class ProxyClient : IProxyClient
{
    TcpClient client = new TcpClient("127.0.0.1", 8000);
    public ProxyClient()
    {
    }

    public NetworkStream GetStream()
    {
        return client.GetStream();
    }
}

public class TestProxyClient : IProxyClient
{
    List<string> stream = new List<string>();
    public TestProxyClient()
    {
    }

    public List<string> GetStream()
    {
        return stream;
    }
}