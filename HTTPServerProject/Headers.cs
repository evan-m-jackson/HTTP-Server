using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HTTPServerProject.ReadStreams;

namespace HTTPServerProject.Headers
{

    public class Header
    {
        IReadStreams reader = default!;
        List<string> headers = new List<string>();

        public Header(IReadStreams r)
        {
            reader = r;
        }

        public string GetLine()
        {
            var line = reader.ReadLine();
            Console.WriteLine(line);
            return line;
        }

        public List<string> GetHeaders()
        {
            var input = reader.ReadLine();

            do
            {
                headers.Add(input);
                Console.WriteLine(input);
                input = reader.ReadLine();
            } while (input != "");
            Console.WriteLine(headers);
            return headers;
        }

    }
}