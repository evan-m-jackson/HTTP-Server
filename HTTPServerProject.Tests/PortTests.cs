using HTTPServerProject.Ports;

namespace HTTPServerProject.Tests.Ports;

public class PortTests
{
    [Fact]
    public void GetPortNumberTest()
    {
        var arg = new string[] { "8000" };
        var newPort = new Port();
        var actual = newPort.GetPort(arg);
        var expected = 8000;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetPort5000DefaultTest()
    {
        var arg = new string[] {};
        var newPort = new Port();
        var actual = newPort.GetPort(arg);
        var expected = 5000;
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetPort5000InvalidTest()
    {
        var arg = new string[] { "Hello World" };
        var newPort = new Port();
        var actual = newPort.GetPort(arg);
        var expected = 5000;
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetPort5000OutOfRangeTest()
    {
        var arg = new string[] { "65536" };
        var newPort = new Port();
        var actual = newPort.GetPort(arg);
        var expected = 5000;
        Assert.Equal(expected, actual);
    }
}