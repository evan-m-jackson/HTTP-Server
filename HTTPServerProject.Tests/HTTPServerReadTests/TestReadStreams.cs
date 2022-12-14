using HTTPServerRead.Streams;

namespace HTTPServerProject.Tests;

public class TestReadStreams : IReadStreams
{
    List<string> sArr = new List<string>();
    public bool closeCalled = false;

    public TestReadStreams(List<string> arr)
    {
        sArr = arr;
    }

    public string ReadLine()
    {
        if (sArr.Count > 0)
        {
            var input = sArr[0];
            sArr.RemoveAt(0);
            return input;
        }

        return "";
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

    public string ReadToEnd()
    {
        var result = "";

        while (sArr.Count > 0)
        {
            var input = sArr[0];
            result += input;
            sArr.RemoveAt(0);
        }

        return result;
    }

    public int Peek()
    {
        if (sArr.Count == 0)
        {
            return -1;
        }
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
        closeCalled = true;
    }
}