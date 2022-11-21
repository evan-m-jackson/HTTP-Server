using HTTPServerProject.WriteStream;

namespace HTTPServerProject.Responses.Body;

public class ResponseBody
{
    IWriteStreams writer;
    public ResponseBody(IWriteStreams w)
    {
        writer = w;
    }

    public void WriteResponseBody(string input)
    {
        if (input.Length > 0)
        {
            writer.Write(input);
            writer.Flush();
        }
        else
        {
            writer.Flush();
        }

    }
}