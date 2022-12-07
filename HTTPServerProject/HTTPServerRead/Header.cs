using HTTPServerRead.Streams;

namespace HTTPServerRead.Header;

    public class Header
    {
        IReadStreams _reader;
        List<string> _headers = new List<string>();

        public Header(IReadStreams reader)
        {
            _reader = reader;
        }

        public string GetLine()
        {
            var line = _reader.ReadLine();
            return line;
        }

        public List<string> GetHeaders()
        {
            var input = _reader.ReadLine();
            while (input != "")
            {
                _headers.Add(input);
                input = _reader.ReadLine();
            } 
            return _headers;
        }

        public string GetRequestType(string line)
        {
            var result = "";
            foreach (char c in line)
            {
                if (c == ' ')
                {
                    break;
                }
                else
                {
                    result += c;
                }
            }
            return result;
        }

        public string GetPath(string line)
        {
            var read = false;
            var result = "";
            foreach (char c in line)
            {
                if (!read && c == '/')
                {
                    read = true;
                }
                else if (read && c == ' ')
                {
                    break;
                }
                else if (read)
                {
                    result += c;
                }
            }
            return result;
        }
        
        public int GetCode(string statusLine)
        {
            var idx = 0;
            if (statusLine[idx] == '[')
            {
                while ( statusLine[idx] != ']')
                {
                    idx += 1;
                }
                idx += 1;
            } 
            var start = idx + 9;
            var codeString = statusLine.Substring(start,3);
            return Int32.Parse(codeString);
        }
    }