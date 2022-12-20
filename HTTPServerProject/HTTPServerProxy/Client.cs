using System.Net.Sockets;

namespace HTTPServerProxy.Client;

public class ProxyClient
{
    TcpClient client = new TcpClient("127.0.0.1", 8000);

    public NetworkStream GetStream()
    {
        return client.GetStream();
    }
}