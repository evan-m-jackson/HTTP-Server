using HTTPServerWrite.Streams;

namespace HTTPServerWriteTests.Streams
{
    public class TestWriteStreams : IWriteStreams
    {
        List<string> sArr = new List<string>();

        public TestWriteStreams(List<string> arr)
        {
            sArr = arr;
        }

        public void Write(string str = default!)
        {
            if (str == default!)
            {
                sArr.Add("");
            }
            else
            {
                sArr.Add(str);
            }
        }

        public void WriteLine(string str = default!)
        {
            if (str == default!)
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