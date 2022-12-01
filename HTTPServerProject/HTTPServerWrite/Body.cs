using HTTPServerWrite.Streams;

namespace HTTPServerWrite.Body;

public class WriteBody
{
    IWriteStreams writer;
    public WriteBody(IWriteStreams w)
    {
        writer = w;
    }

    public void WriteBodyToStream(string input)
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