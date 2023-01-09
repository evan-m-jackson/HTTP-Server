using HTTPServerWrite.Streams;

namespace HTTPServerWrite.Body;

public class WriteBody
{
    IWriteStreams _writer;
    public WriteBody(IWriteStreams writer)
    {
        _writer = writer;
    }

    public void WriteBodyToStream(string input)
    {
        if (input.Length > 0)
        {
            _writer.Write(input);
            _writer.Flush();
        }
        else
        {
            _writer.Flush();
        }
    }
}