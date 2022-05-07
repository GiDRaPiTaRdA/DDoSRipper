using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public static class ProxyChecker
    {
        public static List<ProxyData> LoadProxies(string proxyFile)
        {
            //FileStream s = new FileStream(proxyFile,FileMode.Open);

            string[] strings = File.ReadAllLines(proxyFile);

            Regex regex = new Regex("^([a-zA-Z0-9]+) (\\d+\\.\\d+\\.\\d+\\.\\d+) (\\d+)$");

            IEnumerable<Match> a = strings
                .Select(s => regex.Match(s))
                .Where(r => r.Success);

            List<ProxyData> proxies = a.Select(s => new ProxyData
            {
                Protocol = s.Groups[1].Value,
                Ip = s.Groups[2].Value,
                Port = s.Groups[3].Value
            }).ToList();

            return proxies;
        }

        public static bool CheckProxy(ProxyData proxy, string url)
        {
            ProxyClient client = new ProxyClient(proxy.Ip, int.Parse(proxy.Port));

            HttpResponseMessage a = client.GetAsync(url).Result;

            bool result = a.IsSuccessStatusCode;

            return result;
        }
    }
}