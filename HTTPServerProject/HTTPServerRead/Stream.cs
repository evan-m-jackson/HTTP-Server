using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;

namespace HTTPServerRead.Streams;

    public interface IReadStreams
    {
        int Read();
        string ReadLine();
        int Peek();
        void Close();
    }

    public class ReadStreams : IReadStreams
    {
        Stream rStream;
        StreamReader reader;

        public ReadStreams(Stream stream)
        {
            rStream = stream;
            reader = new StreamReader(stream);
        }

        public string ReadLine()
        {
            var input = reader.ReadLine()!;
            return input;
        }

        public int Read()
        {
            var input = reader.Read();
            return input;
        }

        public int Peek()
        {
            var input = reader.Peek();
            return input;
        }

        public void Close()
        {
            reader.Close();
        }
    }