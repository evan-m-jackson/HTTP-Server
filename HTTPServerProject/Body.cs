using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HTTPServerProject.ReadStream;

namespace HTTPServerProject.Request.Body
{
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
            do
            {
                var r = reader.Read();
                var c = (char)r;
                result += c;
                input = reader.Peek();
            } while ((input != -1));

            return result;
        }
    }
}