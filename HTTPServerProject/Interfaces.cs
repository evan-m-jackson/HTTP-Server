using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;

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
        List<string> sArr = default!;
        List<int> iArr = default!;

        public NewStreamReader(Stream stream)
        {
            rStream = stream;
            reader = new StreamReader(stream);
        }

        public NewStreamReader(List<string> arr)
        {
            sArr = arr;
        }

        public NewStreamReader(List<int> arr)
        {
            iArr = arr;
        }

        public abstract string ReadLine();

        public abstract string ReadToEnd();

        public abstract void Close();
    }

    public class MyStreamReader : NewStreamReader
    {
        Stream rStream = new MemoryStream();
        StreamReader reader = default!;
        List<string> sArr = default!;
        List<int> iArr = default!;
        public MyStreamReader(Stream stream) : base(stream)
        {
            rStream = stream;
            reader = new StreamReader(stream);
        }

        public MyStreamReader(List<string> arr) : base(arr)
        {
            sArr = arr;
        }

        public MyStreamReader(List<int> arr) : base(arr)
        {
            iArr = arr;
        }

        public override string ReadLine()
        {
            string input = reader.ReadLine()!;
            return input;
        }

        public override string ReadToEnd()
        {
            var input = reader.ReadToEnd();
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
        List<string> sArr = default!;
        List<int> iArr = default!;
        public TestStreamReader(Stream stream) : base(stream)
        {
            rStream = stream;
            reader = new StreamReader(stream);
        }

        public TestStreamReader(List<string> arr) : base(arr)
        {
            sArr = arr;
        }

        public TestStreamReader(List<int> arr) : base(arr)
        {
            iArr = arr;
        }
        public override string ReadLine()
        {
            string input = sArr[0];
            sArr.RemoveAt(0);
            return input;
        }

        public override string ReadToEnd()
        {
            return "Hello";
        }

        public override void Close()
        {

        }
    }


}