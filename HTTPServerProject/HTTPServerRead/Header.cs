using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using HTTPServerProject.ReadStream;

namespace HTTPServerProject.ReadHeaders;

    public class Header
    {
        IReadStreams reader;
        List<string> headers = new List<string>();

        public Header(IReadStreams r)
        {
            reader = r;
        }

        public string GetLine()
        {
            var line = reader.ReadLine();
            Console.WriteLine(line);
            return line;
        }

        public List<string> GetHeaders()
        {
            var input = reader.ReadLine();
            do
            {
                headers.Add(input);
                input = reader.ReadLine();
            } while (input != "");

            return headers;
        }

        public string GetRequestType(string line)
        {
            var result = "";
            foreach (char c in line)
            {
                if (c == ' ')
                {
                    break;
                }
                else
                {
                    result += c;
                }
            }
            return result;
        }

        public string GetPath(string line)
        {
            var read = false;
            var result = "";

            foreach (char c in line)
            {
                if (!read && c == '/')
                {
                    read = true;
                }
                else if (read && c == ' ')
                {
                    break;
                }
                else if (read)
                {
                    result += c;
                }
            }
            return result;
        }
        
        public int GetCode(string statusLine)
        {
            var codeString = statusLine.Substring(9,3);
            return Int32.Parse(codeString);
        }
    }