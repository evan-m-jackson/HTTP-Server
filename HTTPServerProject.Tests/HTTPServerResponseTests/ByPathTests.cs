using HTTPServerResponse.Path;

namespace HTTPServerProject.Tests;

public class UnitTestsForResponsesByPath
{
    public List<string> eStream = new List<string>();

    [Theory]
    [MemberData(nameof(Data))]
    public void GetResponseByPathTests(List<string> stream, string path, string method, string body)
    {
    	var expected = stream;
        var writer = new TestWriteStreams(eStream);
        var httpPath = path;
        var httpMethod = method;
        var requestBody = body;
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;

        var responsePath = new ResponsePath(writer, pathDict);
        responsePath.Execute(path: httpPath, type: httpMethod, requestBody: requestBody);

        Assert.Equal(eStream, expected);
    }

    public static IEnumerable<object[]> Data()
    {
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 301 Moved Permanently",
	"Location: http://127.0.0.1:5000/simple_get",
	""
	}, "redirect", "GET", ""};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Allow: GET, HEAD, OPTIONS",
	""
	}, "method_options", "OPTIONS", ""};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Allow: GET, HEAD, OPTIONS, PUT, POST",
	""
	}, "method_options2", "OPTIONS", ""};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Allow: HEAD, OPTIONS",
	""
	}, "head_request", "HEAD", ""};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 405 Method Not Allowed",
	"Allow: HEAD, OPTIONS",
	""
	}, "head_request", "GET", ""};
	yield return new object[] {new List<string>() { "HTTP/1.1 200 OK", "" }, "simple_get", "GET", ""};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"",
	"Hello world"
	}, "simple_get_with_body", "GET", ""};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"",
	"Goodbye"
	}, "echo_body", "GET", "Goodbye"};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Content-Type: text/plain;charset=utf-8",
	"",
	"text response"
	}, "text_response", "GET", ""};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Content-Type: text/html;charset=utf-8",
	"",
	"<html><body><p>HTML Response</p></body></html>"
	}, "html_response", "GET", ""};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Content-Type: application/json;charset=utf-8",
	"",
	"{\"key1\":\"value1\",\"key2\":\"value2\"}"
	}, "json_response", "GET", ""};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Content-Type: application/xml;charset=utf-8",
	"",
	"<note><body>XML Response</body></note>"
	}, "xml_response", "GET", ""};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Content-Type: image/jpeg",
	"",
	"kitteh.jpg"
	}, "kitteh.jpg", "GET", ""};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Content-Type: image/png",
	"",
	"doggo.png"
	}, "doggo.png", "GET", ""};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Content-Type: image/gif",
	"",
	"kisses.gif"
	}, "kisses.gif", "GET", ""};
	yield return new object[] {new List<string>()
	{
	"HTTP/1.1 200 OK",
	"Content-Type: text/html;charset=utf-8",
	"",
	"health-check.html"
	}, "health-check.html", "GET", ""};
    }
}
