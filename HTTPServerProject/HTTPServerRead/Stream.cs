namespace HTTPServerRead.Streams;

    public interface IReadStreams
    {
        int Read();
        string ReadLine();
        int Peek();
        void Close();
        string ReadToEnd();
    }

    public class ReadStreams : IReadStreams
    {
        Stream _stream;
        StreamReader _reader;

        public ReadStreams(Stream stream)
        {
            _stream = stream;
            _reader = new StreamReader(stream);
        }

        public string ReadLine()
        {
            var input = _reader.ReadLine()!;
            return input;
        }

        public int Read()
        {
            var input = _reader.Read();
            return input;
        }

        public string ReadToEnd()
        {
            var input = _reader.ReadToEnd();
            return input;
        }

        public int Peek()
        {
            var input = _reader.Peek();
            return input;
        }

        public void Close()
        {
            _reader.Close();
        }
    }