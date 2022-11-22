using HTTPServerProject.Proxy.Client;

namespace HTTPServerProject.Tests.Proxy.Client;

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