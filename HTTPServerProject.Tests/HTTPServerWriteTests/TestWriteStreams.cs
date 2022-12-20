using HTTPServerWrite.Streams;

namespace HTTPServerWriteTests.Streams
{
    public class UnitTestsForWriteStreams
    {
        [Fact]
        public void WriterIsClosedTest()
        {
            var stream = new List<string>();
            var writer = new TestWriteStreams(stream);
            writer.Close();
            var writerHasClosed = writer.closeCalled;

            Assert.True(writerHasClosed);
        }    
    }
    
    public class TestWriteStreams : IWriteStreams
    {
        List<string> sArr = new List<string>();
        public bool flushCalled = false;
        public bool closeCalled = false;

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
            flushCalled = true;
        }

        public void Close()
        {
            closeCalled = true;
        }
    }
}