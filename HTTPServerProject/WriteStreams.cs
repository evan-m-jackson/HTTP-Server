using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;

namespace HTTPServerProject.WriteStreams
{
    public interface IWriteStreams
    {
        void WriteLine(string str = default!);
        void Flush();
        void Close();
    }

    public class MyStreamWriter : IWriteStreams
    {
        Stream rStream = new MemoryStream();
        StreamWriter writer = default!;

        public MyStreamWriter(Stream stream)
        {
            rStream = stream;
            writer = new StreamWriter(stream);
        }

        public void WriteLine(string str = default!)
        {
            if(str == default!)
            {
                writer.WriteLine();
            }
            else
            {
                writer.WriteLine(str);
            }
        }

        public void Flush()
        {
            writer.Flush();
        }

        public void Close()
        {
            writer.Close();
        }

    }

    public class TestStreamWriter : IWriteStreams
    {
        List<string> sArr = default!;

        public TestStreamWriter(List<string> arr)
        {
            sArr = arr;
        }

        public void WriteLine(string str = default!)
        {
            if(str == default!)
            {
                sArr.Add("");
            }
            else
            {
                sArr.Add(str);
            }
        }

        public void Flush()
        {
        }

        public void Close()
        {
        }
    }
}