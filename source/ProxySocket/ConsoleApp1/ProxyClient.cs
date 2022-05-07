using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ProxyClient: IDisposable
    {
        private HttpClient Client { get; }

        public ProxyClient(string proxyHost, int proxyPort)
        {
            string uri = $"socks5://{proxyHost}:{proxyPort}";

            // Handler
            HttpClientHandler handler = new HttpClientHandler
            {
                Proxy = new WebProxy{Address = new Uri(uri)},

                //UseProxy = true
            };

            // Client
            this.Client = new HttpClient(handler);

            // Auth
            byte[] byteArray = Encoding.ASCII.GetBytes("username:password1234");

            this.Client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task<HttpResponseMessage> GetAsync(string url) => 
            await this.Client.GetAsync(url);

        public void Dispose()
        {
            this.Client?.Dispose();
        }
    }
}