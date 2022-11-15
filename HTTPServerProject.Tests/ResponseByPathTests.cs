namespace HTTPServerProject.ResponseByPathTests;

public class UnitTestsForResponsesByPath
{
    public List<string> eStream = new List<string>();

    [Fact]
    public void GetRedirectResponse()
    {
        var expected = new List<string>() { "HTTP/1.1 301 Moved Permanently", "Location: http://127.0.0.1:5000/simple_get", ""};
        var writer = new TestWriteStreams(eStream);
        var path = "redirect";
        var type = "GET";
        var requestBody = "";

        var requestPath = new RequestPath(writer);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void GetMethodOptionsResponse()
    {
        var expected = new List<string>() { "HTTP/1.1 200 OK", "Allow: GET, HEAD, OPTIONS", ""};
        var writer = new TestWriteStreams(eStream);
        var path = "method_options";
        var type = "OPTIONS";
        var requestBody = "";

        var requestPath = new RequestPath(writer);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void GetMethodOptions2Response()
    {
        var expected = new List<string>() { "HTTP/1.1 200 OK", "Allow: GET, HEAD, OPTIONS, PUT, POST", ""};
        var writer = new TestWriteStreams(eStream);
        var path = "method_options2";
        var type = "OPTIONS";
        var requestBody = "";

        var requestPath = new RequestPath(writer);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void GetHeadRequestHeadResponse()
    {
        var expected = new List<string>() { "HTTP/1.1 200 OK", "Allow: HEAD, OPTIONS", ""};
        var writer = new TestWriteStreams(eStream);
        var path = "head_request";
        var type = "HEAD";
        var requestBody = "";

        var requestPath = new RequestPath(writer);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void GetHeadRequestGetResponse()
    {
        var expected = new List<string>() { "HTTP/1.1 405 Method Not Allowed", "Allow: HEAD, OPTIONS", ""};
        var writer = new TestWriteStreams(eStream);
        var path = "head_request";
        var type = "GET";
        var requestBody = "";

        var requestPath = new RequestPath(writer);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void GetSimpleGetResponse()
    {
        var expected = new List<string>() { "HTTP/1.1 200 OK", ""};
        var writer = new TestWriteStreams(eStream);
        var path = "simple_get";
        var type = "GET";
        var requestBody = "";

        var requestPath = new RequestPath(writer);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void GetSimpleGetWithBodyResponse()
    {
        var expected = new List<string>() { "HTTP/1.1 200 OK", "", "Hello world"};
        var writer = new TestWriteStreams(eStream);
        var path = "simple_get_with_body";
        var type = "GET";
        var requestBody = "";

        var requestPath = new RequestPath(writer);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void GetEchoBodyResponse()
    {
        var expected = new List<string>() { "HTTP/1.1 200 OK", "", "Goodbye"};
        var writer = new TestWriteStreams(eStream);
        var path = "echo_body";
        var type = "GET";
        var requestBody = "Goodbye";

        var requestPath = new RequestPath(writer);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void GetTextResponse()
    {
        var expected = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: text/plain;charset=utf-8", "", "text response"};
        var writer = new TestWriteStreams(eStream);
        var path = "text_response";
        var type = "GET";
        var requestBody = "";

        var requestPath = new RequestPath(writer);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }    

    [Fact]
    public void GetHTMLResponse()
    {
        var expected = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: text/html;charset=utf-8", "", "<html><body><p>HTML Response</p></body></html>"};
        var writer = new TestWriteStreams(eStream);
        var path = "html_response";
        var type = "GET";
        var requestBody = "";

        var requestPath = new RequestPath(writer);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void GetJsonResponse()
    {
        var expected = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: application/json;charset=utf-8", "", "{\"key1\":\"value1\",\"key2\":\"value2\"}"};
        var writer = new TestWriteStreams(eStream);
        var path = "json_response";
        var type = "GET";
        var requestBody = "";

        var requestPath = new RequestPath(writer);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void GetXmlResponse()
    {
        var expected = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: application/xml;charset=utf-8", "", "<note><body>XML Response</body></note>"};
        var writer = new TestWriteStreams(eStream);
        var path = "xml_response";
        var type = "GET";
        var requestBody = "";

        var requestPath = new RequestPath(writer);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

}