namespace HTTPServerProject.Ports;

public class Port
{
    public Port() { }
    
    public int GetPort(string[] args)
    {
        var port = GetPortNum(args);
        if (IsPortValid(port))
        {
            return port;
        }
        else
        {
            return 5000;
        }
    }

    public bool IsPortValid(int port)
    {
        if (port >= 0 && port <= 65535)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public int GetPortNum(string[] args)
    {
        try
        {
            var portStr = args[0];
            return Int32.Parse(portStr);
        }
        catch
        {
            return 5000;
        }
    }
}