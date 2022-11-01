using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HTTPServerProject.WriteStreams;

namespace HTTPServerProject.Responses
{
    public class Response
    {
        IWriteStreams writer = default!;

        public Response(IWriteStreams w)
        {
            writer = w;
        }

        public void WriteResponse(string input)
        {
            WriteResponseHeader();
            writer.WriteLine(input);
        }

        public void WriteResponseHeader()
        {
            writer.WriteLine("HTTP/1.1 200 OK");
            writer.WriteLine();
        }
   
    }
}