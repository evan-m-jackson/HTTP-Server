using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HTTPServerProject.ReadStreams;

namespace HTTPServerProject.Request.Body
{
    public class Body
    {
        IReadStreams reader = default!;

        public Body(IReadStreams r)
        {
            reader = r;
        }

        public string GetBody()
        {
            int input = reader.Peek();
            var result = "";
            do
            {
                int r = reader.Read();
                char c = (char)r;
                result += c;
                input = reader.Peek();
            } while ((input != -1));

            return result;
        }
    }
}