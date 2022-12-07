namespace HTTPServerWrite.Streams;

    public interface IWriteStreams
    {
        void Write(string str = default!);
        void WriteLine(string str = default!);
        void Flush();
        void Close();
    }

    public class WriteStreams : IWriteStreams
    {
        Stream _stream = new MemoryStream();
        StreamWriter _writer;

        public WriteStreams(Stream stream)
        {
            _stream = stream;
            _writer = new StreamWriter(stream);
        }

        public void Write(string str = default!)
        {
            _writer.Write(str);
        }

        public void WriteLine(string str = default!)
        {
            if (str == default!)
            {
                _writer.WriteLine();
            }
            else
            {
                _writer.WriteLine(str);
            }
        }

        public void Flush()
        {
            _writer.Flush();
        }

        public void Close()
        {
            _writer.Close();
        }
    }