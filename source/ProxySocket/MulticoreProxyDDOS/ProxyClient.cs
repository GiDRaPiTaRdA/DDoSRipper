using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MulticoreProxyDDOS.Client
{
    public class ProxyClient1
    {
        private HttpClient Client { get; }


        public ProxyClient1(string proxyHost, int proxyPort)
        {
            // Handler
            HttpClientHandler handler = new HttpClientHandler
            {
                Proxy = new WebProxy(proxyHost, proxyPort),
                UseProxy = true,
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

        public void Test()
        {
        }
    }
}