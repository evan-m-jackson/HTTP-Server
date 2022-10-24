using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HTTPServerProject.Interfaces;

namespace HTTPServerProject.Headers
{

    public class Header
    {
        NewStreamReader reader = default!;
        List<string> headers = new List<string>();

        public Header(NewStreamReader r)
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

            while(input != "")
            {
                headers.Add(input);
                Console.WriteLine(input);
                input = reader.ReadLine();
            }

            return headers;
        }

    }
}