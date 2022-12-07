using HTTPServerRead.Streams;

namespace HTTPServerRead.Body;

    public class Body
    {
        IReadStreams _reader;

        public Body(IReadStreams reader)
        {
            _reader = reader;
        }

        public string GetBody()
        {
            var input = _reader.Peek();
            var result = "";
            while ((input != -1))
            {
                var r = _reader.Read();
                var c = (char)r;
                result += c;
                input = _reader.Peek();
            }
            return result;
        }
    }