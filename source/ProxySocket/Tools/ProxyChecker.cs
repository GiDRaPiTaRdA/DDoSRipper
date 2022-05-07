using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tools.ProxyParsers;


namespace Tools
{
    public static class ProxyChecker
    {
        const string proxyCheckUrl = "https://www.google.com/";

        public static List<ProxyData> LoadProxies(string proxyFile, IProxyParser parser)
        {
            string[] strings = File.ReadAllLines(proxyFile);

            List<ProxyData> proxies = LoadProxies(strings, parser);

            return proxies;
        }

        public static List<ProxyData> LoadProxies(string[] strings, IProxyParser parser)
        {
            List<ProxyData> proxies =
                strings
                    .Select(s => parser.Regex.Match(s))
                    .Where(r => r.Success)
                    .Select(parser.Parse)
                    .ToList();

            return proxies;
        }


        public static HttpStatusCode? CheckProxy(ProxyClient client, string url = proxyCheckUrl, int retry = 1)
        {
            HttpStatusCode? result = null;

            for (int i = 0; i < retry; i++)
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;

                    result = response.StatusCode;
                }
                catch (Exception e)
                {
                    // ignored
                }
            }

            return result;
        }
        public static HttpStatusCode? CheckProxy(ProxyData proxy, string url = proxyCheckUrl, int retry = 1)
        {
            HttpStatusCode? result = null;

            using (ProxyClient client = new ProxyClient(proxy.Protocol, proxy.Ip, proxy.Port))
            {
                result = CheckProxy(client, url, retry);
            }

            return result;
        }


        /// <summary>
        /// trash
        /// </summary>
        /// <param name="proxiesToCheck"></param>
        /// <param name="retry"></param>
        /// <returns></returns>
        public static List<Tuple<HttpStatusCode?, ProxyData>> CheckProxies(IEnumerable<ProxyData> proxiesToCheck, int retry)
        {
            List<Tuple<HttpStatusCode?, ProxyData>> proxyCheckResult = proxiesToCheck
                .AsParallel()
                .Select(selector: p => CheckProxy(proxyCheckUrl, p, retry))
                .ToList();

            return proxyCheckResult;

            Tuple<HttpStatusCode?, ProxyData> CheckProxy(string url, ProxyData p, int retry1)
            {
                Stopwatch s = Stopwatch.StartNew();

                Tuple<HttpStatusCode?, ProxyData> checkedProxy =
                    new Tuple<HttpStatusCode?, ProxyData>(ProxyChecker.CheckProxy(p, url, retry1), p);

                Console.WriteLine(
                    $"Check proxy \t{checkedProxy.Item2.Protocol.ToUpper()} {checkedProxy.Item2.Ip} \t {checkedProxy.Item2.Port} \t {(checkedProxy.Item1 != null ? checkedProxy.Item1.ToString() : "Fail")} \t{s.ElapsedMilliseconds / 1000}s",
                    (checkedProxy.Item1 == HttpStatusCode.OK ? ConsoleColor.Green : ConsoleColor.DarkRed));

                return checkedProxy;
            }
        }

        public static List<ProxyData> CheckProxies(IEnumerable<ProxyData> proxiesToCheck, int concurrencyLevel, int take)
        {
            List<ProxyData> checkProxies = new List<ProxyData>();

            ConcurrentStack<ProxyData> concurrentStack = new ConcurrentStack<ProxyData>(proxiesToCheck);

            Task[] tasks = new Task[concurrencyLevel];

            System.Console.WriteLine($"Run checkers {tasks.Length}");

            for (int i = 0; i < tasks.Length; i++)
            {
                Task task = Task.Factory.StartNew(() =>
                {
                    while (concurrentStack.TryPop(out ProxyData data))
                    {
                        HttpStatusCode? code = Check(data);

                        if (checkProxies.Count >= take)
                        {
                            Console.WriteLine($"Stop checker", ConsoleColor.Red);
                            break;
                        }

                        if (code == HttpStatusCode.OK)
                            checkProxies.Add(data);
                    }
                });

                tasks[i] = task;
            }

            Task.WaitAll(tasks);

            return checkProxies;

            HttpStatusCode? Check(ProxyData data)
            {
                Stopwatch s = Stopwatch.StartNew();

                HttpStatusCode? code = CheckProxy(data);

                Console.WriteLine(
                    $"Check proxy \t{data.Protocol.ToUpper()} {data.Ip} \t {data.Port} \t " +
                    $"{(code != null ? code.ToString() : "Fail")} " +
                    $"\t{s.ElapsedMilliseconds / 1000}s",
                    (code == HttpStatusCode.OK ? ConsoleColor.Green : ConsoleColor.DarkRed));

                return code;
            }
        }


        public static ProxyClient ConnectProxy(IEnumerable<ProxyData> proxies, Action<ProxyData, HttpStatusCode?, long> Callback)
        {
            ProxyClient connectedClient = null;

            foreach (ProxyData proxy in proxies)
            {
                Stopwatch s = Stopwatch.StartNew();

                ProxyClient client = new ProxyClient(proxy.Protocol, proxy.Ip, proxy.Port);

                HttpStatusCode? code = CheckProxy(client); Callback(proxy, code, s.ElapsedMilliseconds);

                if (code == HttpStatusCode.OK)
                {
                    connectedClient = client;
                    break;
                }
                else
                {
                    client.Dispose();
                }
            }

            return connectedClient;
        }

        public static List<ProxyClient> ConnectProxies(IEnumerable<ProxyData> proxies)
        {
            List<ProxyClient> clients = new List<ProxyClient>();

            foreach (ProxyData proxy in proxies)
            {
                HttpStatusCode? code = null;
                ProxyClient client = null;

                try
                {
                    client = new ProxyClient(proxy.Protocol, proxy.Ip, proxy.Port);

                    code = CheckProxy(client);
                }
                finally
                {
                    if (code == HttpStatusCode.OK)
                    {
                        clients.Add(client);
                    }
                    else
                    {
                        client?.Dispose();
                    }
                }
            }

            return clients;
        }


        public static List<ProxyClient> ConnectProxiesParallel(IEnumerable<ProxyData> proxies)
        {
            List<ProxyClient> clients = new List<ProxyClient>();

            proxies.AsParallel().ForAll(proxy =>
            {
                ProxyClient client = null;
                bool connected = false;

                try
                {
                    client = new ProxyClient(proxy.Protocol, proxy.Ip, proxy.Port);

                    HttpStatusCode? code = CheckProxy(client);

                    connected = code == HttpStatusCode.OK;
                }
                finally
                {
                    if (connected)
                    {
                        clients.Add(client);
                    }
                    else
                    {
                        client?.Dispose();
                    }
                }
            });

            return clients;
        }

        public static List<ProxyClient> ConnectProxiesParallelLinq(IEnumerable<ProxyData> proxies)
        {
            List<ProxyClient> clients = proxies.AsParallel()
                .Select(proxy => new ProxyClient(proxy.Protocol, proxy.Ip, proxy.Port))
                .Where(client =>
                {
                    bool connected = false;

                    try
                    {
                        HttpStatusCode? code = CheckProxy(client);

                        connected = code == HttpStatusCode.OK;
                    }
                    finally
                    {
                        if (!connected)
                        {
                            client?.Dispose();
                        }
                    }

                    return connected;
                })
                .ToList();

            return clients;
        }


        // ReSharper disable once InconsistentNaming
        public static List<ProxyClient> ConnectProxies(IEnumerable<ProxyData> proxies, int take, Action<ProxyData, HttpStatusCode?, long> Callback)
        {
            const int concurrencyLevel = 12;

            List<ProxyClient> clients = new List<ProxyClient>();

            ConcurrentStack<ProxyData> concurrentStack = new ConcurrentStack<ProxyData>(proxies);

            Task[] tasks = new Task[concurrencyLevel];

            for (int i = 0; i < tasks.Length; i++)
            {
                Task task = Task.Factory.StartNew(() =>
                {
                    while (concurrentStack.TryPop(out ProxyData proxy) && clients.Count < take)
                    {
                        bool connected = false;

                        ProxyClient client = null;

                        try
                        {
                            Stopwatch s = Stopwatch.StartNew();

                            client = new ProxyClient(proxy.Protocol, proxy.Ip, proxy.Port);

                            HttpStatusCode? code = CheckProxy(client); Callback(proxy, code, s.ElapsedMilliseconds);

                            connected = code == HttpStatusCode.OK;
                        }
                        finally
                        {
                            if (!connected)
                            {
                                client?.Dispose();
                            }
                            else
                            {
                                if (clients.Count < take)
                                {
                                    clients.Add(client);
                                }
                            }
                        }
                    }
                });

                tasks[i] = task;
            }

            Task.WaitAll(tasks);

            return clients;
        }
    }
}