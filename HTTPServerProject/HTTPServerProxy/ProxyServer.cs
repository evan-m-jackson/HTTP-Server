using HTTPServerProject.WriteStream;

namespace HTTPServerProject.Proxy.Server;

public class ProxyServer
{
    IWriteStreams streamWriter;
    string initialLine;
    List<string> reqHeaders;
    string reqBody;
    
    public ProxyServer(IWriteStreams writer, string iL, List<string> rH, string rB)
    {
        streamWriter = writer;
        initialLine = iL;
        reqHeaders = rH;
        reqBody = rB;
    }

    public void WriteRequest()
    {
        streamWriter.WriteLine(initialLine);
        WriteRequestHeaders();
        WriteRequestBody();
    }

    public void WriteRequestHeaders()
    {
        foreach(var header in reqHeaders)
        {
            streamWriter.WriteLine(header);
        }
        streamWriter.WriteLine();
    }

    public void WriteRequestBody()
    {
        if (reqBody.Length > 0)
        {
            streamWriter.Write(reqBody);
        }
        streamWriter.Flush();
    }
}