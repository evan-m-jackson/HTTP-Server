using HTTPServerRead.Streams;
using HTTPServerRead.Header;
using HTTPServerRead.Body;
using HTTPServerWrite.Request;
using HTTPServerWrite.Streams;

namespace HTTPServerProxy.FirstID;

public class GetFirstID
{
    string _body;

    public GetFirstID(string body)
    {
        _body = body;
    }

	public string Run()
    {
        if (ListEmpty())
        {
            return "1";
        }
        var result = GetID();
        return result;
	}
    public string GetID()
    {
        var foundInt = false;
        var result = "";
        foreach (char c in _body)
        {
            if (Char.IsDigit(c) && !foundInt)
            {
                foundInt = true;
                result += c;
            }
            else if (Char.IsDigit(c))
            {
                result += c;
            }
            else if (foundInt && !Char.IsDigit(c))
            {
                break;
            }
        }
        return result;
    }

	public bool ListEmpty()
    {
        var todoListLength = _body.Length;
        return todoListLength == 2;
    }
}