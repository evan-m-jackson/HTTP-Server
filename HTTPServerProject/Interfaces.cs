using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace HTTPServerProject.Interfaces
{
    public interface INewStreamReader
    {
        string ReadLine();
        void Close();
    }
    public abstract class NewStreamReader : INewStreamReader
    {
        Stream rStream = new MemoryStream();
        StreamReader reader = default!;
        List<string> rArr = default!;

        public NewStreamReader(Stream stream, List<string> arr = default!)
        {
            rStream = stream;
            reader = new StreamReader(stream);
            rArr = arr;
        }

        public abstract string ReadLine();

        public abstract void Close();
    }

    public class MyStreamReader : NewStreamReader
    {
        Stream rStream = new MemoryStream();
        StreamReader reader = default!;
        List<string> rArr = default!;
        public MyStreamReader(Stream stream, List<string> arr = default!) : base(stream, arr)
        {
            rStream = stream;
            reader = new StreamReader(stream);
            rArr = arr;
        }
        public override string ReadLine()
        {
            string input = reader.ReadLine()!;
            return input;
        }

        public override void Close()
        {
            reader.Close();
        }
    }

    public class TestStreamReader : NewStreamReader
    {
        Stream rStream = new MemoryStream();
        StreamReader reader = default!;
        List<string> rArr = default!;
        public TestStreamReader(Stream stream, List<string> arr = default!) : base(stream, arr)
        {
            rStream = stream;
            reader = new StreamReader(stream);
            rArr = arr;

        }
        public override string ReadLine()
        {
            string input = rArr[0];
            rArr.RemoveAt(0);
            return input;
        }

        public override void Close()
        {

        }
    }


}