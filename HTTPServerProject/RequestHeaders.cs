using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HTTPServerProject.ReadStream;

namespace HTTPServerProject.RequestHeaders
{

    public class Header
    {
        IReadStreams reader;
        List<string> headers = new List<string>();

        public Header(IReadStreams r)
        {
            reader = r;
        }

        public string GetLine()
        {
            var line = reader.ReadLine();
            return line;
        }

        public List<string> GetHeaders()
        {
            var input = reader.ReadLine();

            do
            {
                headers.Add(input);
                input = reader.ReadLine();
            } while (input != "");

            return headers;
        }

    }
}