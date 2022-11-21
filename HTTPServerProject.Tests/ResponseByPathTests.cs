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
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;

        var requestPath = new RequestPath(writer, pathDict);
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
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;

        var requestPath = new RequestPath(writer, pathDict);
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
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;

        var requestPath = new RequestPath(writer, pathDict);
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
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;

        var requestPath = new RequestPath(writer, pathDict);
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
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;

        var requestPath = new RequestPath(writer, pathDict);
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
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;

        var requestPath = new RequestPath(writer, pathDict);
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
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;

        var requestPath = new RequestPath(writer, pathDict);
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
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;

        var requestPath = new RequestPath(writer, pathDict);
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
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;

        var requestPath = new RequestPath(writer, pathDict);
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
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;

        var requestPath = new RequestPath(writer, pathDict);
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
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;

        var requestPath = new RequestPath(writer, pathDict);
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
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;

        var requestPath = new RequestPath(writer, pathDict);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void GetKittehResponse()
    {
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;
        var expected = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: image/jpeg", "", "kitteh.jpg"};
        var writer = new TestWriteStreams(eStream);
        var path = "kitteh.jpg";
        var type = "GET";
        var requestBody = "";

        var requestPath = new RequestPath(writer, pathDict);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void GetDoggoResponse()
    {
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;
        var expected = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: image/png", "", "doggo.png"};
        var writer = new TestWriteStreams(eStream);
        var path = "doggo.png";
        var type = "GET";
        var requestBody = "";

        var requestPath = new RequestPath(writer, pathDict);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void GetKissesResponse()
    {
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;
        var expected = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: image/gif", "", "kisses.gif"};
        var writer = new TestWriteStreams(eStream);
        var path = "kisses.gif";
        var type = "GET";
        var requestBody = "";

        var requestPath = new RequestPath(writer, pathDict);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void GetHealthCheckResponse()
    {
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;
        var expected = new List<string>() { "HTTP/1.1 200 OK", "Content-Type: text/html;charset=utf-8", "", "health-check.html"};
        var writer = new TestWriteStreams(eStream);
        var path = "health-check.html";
        var type = "GET";
        var requestBody = "";

        var requestPath = new RequestPath(writer, pathDict);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void GetToDoTaskResponse()
    {
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;
        var expected = new List<string>() { "HTTP/1.1 201 Created", "Content-Type: application/json;charset=utf-8", "", "{\"key1\":\"value1\",\"key2\":\"value2\"}"};
        var writer = new TestWriteStreams(eStream);
        var path = "todo";
        var type = "POST";
        var requestBody = "{\"key1\":\"value1\",\"key2\":\"value2\"}";

        var requestPath = new RequestPath(writer, pathDict);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void GetMethodNotAllowedTest()
    {
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;
        var expected = new List<string>() { "HTTP/1.1 405 Method Not Allowed",  ""};
        var writer = new TestWriteStreams(eStream);
        var path = "todo";
        var type = "GET";
        var requestBody = "";

        var requestPath = new RequestPath(writer, pathDict);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void ToDoTaskUnsupportedTypeTest()
    {
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;
        var expected = new List<string>() { "HTTP/1.1 415 Unsupported Media Type",  ""};
        var writer = new TestWriteStreams(eStream);
        var path = "todo";
        var type = "POST";
        var requestBody = "<task>a new task</task>";

        var requestPath = new RequestPath(writer, pathDict);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }

    [Fact]
    public void ToDoTaskInvalidTest()
    {
        var pathParams = new TestPathParameters();
        var pathDict = pathParams.pathDict;
        var expected = new List<string>() { "HTTP/1.1 400 Bad Request",  ""};
        var writer = new TestWriteStreams(eStream);
        var path = "todo";
        var type = "POST";
        var requestBody = "task: new task";

        var requestPath = new RequestPath(writer, pathDict);
        requestPath.ExecuteRequest(path, type, requestBody);

        Assert.Equal(eStream, expected);
    }
}