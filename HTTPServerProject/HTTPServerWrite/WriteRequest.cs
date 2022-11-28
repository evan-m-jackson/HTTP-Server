using HTTPServerProject.WriteStream;

namespace HTTPServerProject.Requests.Write;

public class WriteRequest
{
    IWriteStreams streamWriter;
    string initialLine;
    List<string> reqHeaders;
    string reqBody;
    
    public WriteRequest(IWriteStreams writer, string iL, List<string> rH, string rB)
    {
        streamWriter = writer;
        initialLine = iL;
        reqHeaders = rH;
        reqBody = rB;
    }

    public void GetRequest()
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