using HTTPServerProject.ReadHeaders;
using HTTPServerProject.ReadBody;
using HTTPServerProject.Tests.ReadStream;
using HTTPServerProject.Tests.WriteStream;
using HTTPServerProject.Proxy.Server;
using HTTPServerProject.Tests.Proxy.Client;

namespace HTTPServerProject.ProxyServerTests;

public class UnitTestsForProxyServer
{

    public List<string> request = new List<string>() { "GET / HTTP/1.1", "Host: localhost:5000", "User-Agent: curl/7.79.1", "Accept: */*", "", "quit" };

    [Fact]
    public void GetProxyRequestTest()
    {
        List<string> expected = new List<string>() { "GET / HTTP/1.1", "Host: localhost:5000", "User-Agent: curl/7.79.1", "Accept: */*", "", "quit" };
        var reader = new TestReadStreams(request);
        var header = new Header(reader);
        var initialLine = header.GetLine();
        var reqHeaders = header.GetHeaders();
        var body = new Body(reader);
        var reqBody = body.GetBody();

        var proxyClient = new TestProxyClient();
        var proxyStream = proxyClient.GetStream();
        var writer = new TestWriteStreams(proxyStream);

        var proxyServer = new ProxyServer(writer, initialLine, reqHeaders, reqBody);
        proxyServer.WriteRequest();

        Assert.Equal(expected, proxyStream);
    }
}