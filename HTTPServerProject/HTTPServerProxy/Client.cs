using System.Net.Sockets;

namespace HTTPServerProxy.Client;

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