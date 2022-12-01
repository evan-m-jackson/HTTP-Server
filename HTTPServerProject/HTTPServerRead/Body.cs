using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HTTPServerRead.Streams;

namespace HTTPServerRead.Body;

    public class Body
    {
        IReadStreams reader;

        public Body(IReadStreams r)
        {
            reader = r;
        }

        public string GetBody()
        {
            var input = reader.Peek();
            var result = "";
            while ((input != -1))
            {
                var r = reader.Read();
                var c = (char)r;
                result += c;
                input = reader.Peek();
            }
            return result;
        }
    }