namespace HTTPServerProject.Ports;

public class Port
{
    public Port() { }
    
    public int GetPort(string[] args)
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