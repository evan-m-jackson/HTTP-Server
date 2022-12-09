using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using HTTPServerRead.Streams;
using HTTPServerProject.Ports;
using HTTPServerProject.Update.Initial.Line;
using HTTPServerRead.Header;
using HTTPServerRead.Body;
using HTTPServerWrite.Response;
using HTTPServerWrite.Streams;
using HTTPServerResponse.Path;
using HTTPServerResponse.Parameters;
using HTTPServerWrite.Request;
using HTTPServerProxy.Check.Paths;
using HTTPServerProxy.Client;
using HTTPServerProxy.Response;
using HTTPServerProxy.FirstID;

namespace HTTPServerProject;

    public class Server
    {

        TcpClient _client;

        public Server(TcpClient tcpc)
        {
            _client = tcpc;
        }

        public void Conversation()
        {
            var stream = _client.GetStream();
            var writer = new WriteStreams(stream);
            var reader = new ReadStreams(stream);

            Console.WriteLine("Connection accepted.");

            var header = new Header(reader);
            var initialLine = header.GetLine();
            var rHeader = header.GetHeaders();

            var requestBody = new Body(reader);
            var bodyString = requestBody.GetBody();

            var httpType = header.GetRequestType(initialLine);
            var httpPath = header.GetPath(initialLine);
            
			var checkPath = new CheckPath();
			var isProxyPath = checkPath.IsTodoPath(httpPath);

            if (isProxyPath)
            {
                var proxyClient = new ProxyClient();
                var proxyStream = proxyClient.GetStream();
                var proxyWriter = new WriteStreams(proxyStream);
                var proxyReader = new ReadStreams(proxyStream);

				var firstId = new GetFirstID(proxyReader, proxyWriter, httpPath, httpType);
				var updateIL = new UpdateInitialLine(proxyReader, proxyWriter, httpPath, httpType, firstId);
				initialLine = updateIL.Run();
				
                var proxyRequest = new WriteRequest(proxyWriter, initialLine, rHeader, bodyString);
                proxyRequest.GetRequest();

                var proxyResponse = new ProxyResponse(proxyReader, writer, httpPath, httpType);
                proxyResponse.GetResponse();
            }
            else
            {
                var pathParams = new PathParameters();
                var pathDict = pathParams.pathDict;

                var execute = new ResponsePath(writer, pathDict);
                execute.ExecuteRequest(httpPath, httpType, bodyString);    
            }

            Console.WriteLine("Closing the connection.");

            reader.Close();
            writer.Close();
            _client.Close();
        }

        public static void Main(string[] args)
        {
			var port = new Port();
			var portNum = port.GetPort(args);
            var listener = new TcpListener(IPAddress.Any, portNum);
            try
            {
                listener.Start();

                Console.WriteLine("Server running on port {0}", portNum);

                while (true)
                {
                    var server = new Server(listener.AcceptTcpClient());

                    var serverThread = new Thread(new ThreadStart(server.Conversation));

                    serverThread.Start();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e + " " + e.StackTrace);
            }
            finally
            {
                listener.Stop();
            }

        }
    }