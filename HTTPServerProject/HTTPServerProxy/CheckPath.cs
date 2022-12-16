namespace HTTPServerProxy.Check.Paths;

public class CheckPath
{
    public CheckPath(){}

    public bool IsTodoPath(string path)
    {
        try
        {
            var result = path.Substring(0, 4) == "todo";
            return result;
        }
        catch
        {
            return false;
        }
    }
}