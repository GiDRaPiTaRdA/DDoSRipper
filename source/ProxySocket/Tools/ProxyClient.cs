using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Extreme.Net;

namespace Tools
{
    public class ProxyClient : IDisposable
    {
        public string Uri { get; }

        private HttpClient Client { get; }

        public event EventHandler<string> OnLog; 

        public ProxyClient(string protocol, string proxyHost, int proxyPort, int timeout = 20*1000)
        {
            protocol = protocol.ToLower();

            this.Uri = $"{protocol}://{proxyHost}:{proxyPort}";

            HttpMessageHandler handler;

            switch (protocol)
            {
                case "http":
                case "https":
                    WebProxy webProxy = new WebProxy(proxyHost, proxyPort);
                    handler = new HttpClientHandler
                    {
                        Proxy = webProxy,
                        UseProxy = true,
                    };


                    break;

                case "socks5":
                    Socks5ProxyClient socksProxy = new Socks5ProxyClient(proxyHost, proxyPort);
                    socksProxy.ConnectTimeout = timeout;
                    handler = new ProxyHandler(socksProxy);
                    break;

                default:
                    throw new ArgumentException($"Protocol {protocol} not supported");
            }

            // Handler


            // Client
            this.Client = new HttpClient(handler);

            this.Client.Timeout = TimeSpan.FromMilliseconds(timeout);

            // Auth
            byte[] byteArray = Encoding.ASCII.GetBytes("username:password1234");

            this.Client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task<HttpResponseMessage> GetAsync(string url) =>
            await this.Client.GetAsync(url);

        private void Log(string message) => 
            this.OnLog?.Invoke(this,message);

        public void Dispose()
        {
            this.Client?.Dispose();
        }
    }
}