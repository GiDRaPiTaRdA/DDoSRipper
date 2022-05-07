using System;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Extreme.Net;

namespace ProxySocks5Client
{
    //https://hidemy.name/ru/proxy-list/
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");
            // Console.ReadKey();

            
            string host = "65.21.153.73";
            int port = 4003;

            Console.WriteLine($"Proxy server:{host}:{port} Protocol: Socks5");

            Ping p = new Ping();
            PingReply reply = p.Send(host, timeout: 10 * 1000);

            if (reply != null)
            {
                Console.WriteLine("Status :  " + reply.Status + " \n Time : " + reply.RoundtripTime.ToString() +
                                  " \n Address : " + reply.Address);
                //Console.WriteLine(reply.ToString());

            }

            Task t = new Task(() => HTTP_GET(host, port));
            t.Start();
            Console.ReadLine();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }


        static async void HTTP_GET(string proxyHost, int proxyPort)
        {
            var TARGETURL = "http://en.wikipedia.org/";

            Socks5ProxyClient socksProxy = new Socks5ProxyClient(proxyHost, proxyPort);
            ProxyHandler handler = new ProxyHandler(socksProxy);

         

            Console.WriteLine("GET: + " + TARGETURL);


            // ... Use HttpClient.            
            HttpClient client = new HttpClient(handler);



            var byteArray = Encoding.ASCII.GetBytes("username:password1234");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            HttpResponseMessage response = await client.GetAsync(TARGETURL);
            HttpContent content = response.Content;

            // ... Check Status Code                                
            Console.WriteLine("Response StatusCode: " + (int) response.StatusCode);

            // ... Read the string.
            string result = await content.ReadAsStringAsync();

            // ... Display the result.
            if (result != null &&
                result.Length >= 50)
            {
                Console.WriteLine(result.Substring(0, 50) + "...");
            }
        }
    }
}



