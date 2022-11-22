using HTTPServerProject.ReadStream;

namespace HTTPServerProject.Tests.ReadStream;

public class TestReadStreams : IReadStreams
{
    List<string> sArr = new List<string>();

    public TestReadStreams(List<string> arr)
    {
        sArr = arr;
    }

    public string ReadLine()
    {
        var input = sArr[0];
        sArr.RemoveAt(0);
        return input;
    }

    public int Read()
    {
        var input = sArr[0];
        if (input == "")
        {
            return -1;
        }
        else
        {
            var c = input[0];
            var ascii_c = (int)c;

            if (input.Length > 1)
            {
                sArr[0] = input.Substring(1);
            }
            else
            {
                sArr[0] = "";
            }

            return ascii_c;
        }
    }

    public int Peek()
    {
        var input = sArr[0];
        if (input == "")
        {
            return -1;
        }
        else
        {
            var c = input[0];
            var ascii_c = (int)c;

            return ascii_c;
        }
    }

    public void Close()
    {

    }
}