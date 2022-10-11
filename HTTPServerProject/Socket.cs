using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Socket
{
    public class TcpSocket
    {
        TcpListener listener = null!;

        public TcpSocket(object client)
        {
            listener = (TcpListener)client;
        }

        public void Start(){
            listener.Start();
        }

        public void Stop()
        {
            listener.Stop();
        }
    }
}

