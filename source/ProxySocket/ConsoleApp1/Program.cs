using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Maxim\Documents\Source\web\proxy.txt";
            string proxyCheckUrl = "https://www.google.com";

            // Load
            List<ProxyData> proxies = ProxyChecker.LoadProxies(path);


            {
                ProxyData proxyData = proxies.First();

                bool r = ProxyChecker.CheckProxy(proxyData, proxyCheckUrl);
            }
        }
    }
}
