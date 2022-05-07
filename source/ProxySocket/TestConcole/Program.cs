using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tools;
using Tools.ProxyParsers;
//using Console = CustomConsole.Console;
using Console = Tools.Console;

namespace TestConcole
{
    class Program
    {
        static void Main(string[] args)
        {
            const int retry = 1;

            //if (args.Length != 1 || !File.Exists(args[0]))
            //{
            //    Console.WriteLine("Invalid input");
            //    return;
            //}

            // Args
            IProxyParser parser = GetParser(args[0]);
            string protocol = args[1];
            int take = int.Parse(args[2]);
            string path = args[3];

            // no pause call
            bool direct = args.Length > 3 && bool.Parse(args[4]);

            // Ansi coloring mode for console
            Console.AnsiColorMode = args.Length > 4 && bool.Parse(args[5]);

            {
                System.Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine($"Proxy parser : {parser}", ConsoleColor.DarkYellow);
                Console.WriteLine($"Proxy protocol : {protocol}", ConsoleColor.DarkYellow);
                Console.WriteLine($"Proxy take : {take}", ConsoleColor.DarkYellow);
                Console.WriteLine($"Proxy list file : {path}\n", ConsoleColor.DarkYellow);
            }

            // Load
            List<ProxyData> proxies = ProxyChecker.LoadProxies(path, parser);
            proxies.ForEach(p => p.Protocol = protocol);
            if (proxies.Count < take)
                take = proxies.Count;

            {
                Console.WriteLine($"Loaded {proxies.Count}");

                if (!direct)
                {
                    Console.WriteLine("Press to start check");
                    Console.ReadLine();
                }
            }

            proxies = proxies.Take(take).ToList();

            // Check
            Console.WriteLine($"Start proxies check count {proxies.Count}");
            List<Tuple<HttpStatusCode?, ProxyData>> proxyCheckResult = ProxyChecker.CheckProxies(proxies, retry);

            System.Console.ResetColor();

            // Output
            {
                IEnumerable<Tuple<HttpStatusCode?, ProxyData>> suceededproxies =
                    proxyCheckResult.Where(p => p.Item1 == HttpStatusCode.OK);

                Console.WriteLine($"\nProxy OK Count {suceededproxies.Count()}", ConsoleColor.Green);


                IEnumerable<Tuple<HttpStatusCode?, ProxyData>> failedProxies =
                    proxyCheckResult.Where(p => p.Item1 != HttpStatusCode.OK);

                Console.WriteLine($"Proxy Failed Count {failedProxies.Count()}", ConsoleColor.Red);


                //if(!direct)

                if (!direct)
                {
                    //Export
                    while (true)
                    {
                        Console.WriteLine("Do you want to export data? (y/n/o)");

                        string cmd = Console.ReadLine();

                        if (cmd == "y")
                        {
                            string dir = Path.GetDirectoryName(path);

                            string file = Path.Combine(dir, $"proxies_{DateTime.Today:dd.MM.yyyy}.txt");

                            File.WriteAllLines(file, suceededproxies
                                .Select(pt => pt.Item2)
                                .Select(p => $"{p.Protocol} {p.Ip} {p.Port}")
                                .ToArray());
                        }
                        else if (cmd == "o")
                        {
                            foreach (Tuple<HttpStatusCode?, ProxyData> suceedProxy in suceededproxies)
                            {
                                Console.WriteLine(
                                    $"{suceedProxy.Item2.Protocol} {suceedProxy.Item2.Ip} {suceedProxy.Item2.Port}");
                            }
                        }
                        else if (cmd == "n")
                        {
                            break;
                        }
                        else
                        {
                            System.Console.WriteLine("Invalid input");
                        }
                    }
                }
                else
                {
                    //string dir = Path.GetDirectoryName(path);

                    //string file = Path.Combine(dir, $"proxies_{DateTime.Today:dd.MM.yyyy}.conf");

                    //string automationDir = @"C:\Users\Maxim\Documents\Source\web\DDoSRipper\automation\";
                    string automationDir = @"automation/proxy/data/";

                    string rawProxyFile = $@"{automationDir}proxychains-raw.conf";
                    string proxyConfFile = $@"{automationDir}proxychains.conf";

                    string[] rawProxy = File.ReadAllLines(rawProxyFile);

                    // write
                    File.WriteAllLines(proxyConfFile, rawProxy);

                    File.AppendAllLines(proxyConfFile, suceededproxies
                        .Select(pt => pt.Item2)
                        .Select(p => $"{p.Protocol} {p.Ip} {p.Port}")
                        .ToArray());

                    Console.WriteLine("\nProxies exported.", ConsoleColor.DarkCyan);
                    Console.WriteLine($"To : {proxyConfFile}",ConsoleColor.DarkCyan);
                }
            }
        }


        private static IProxyParser GetParser(string parserString)
        {
            switch (parserString)
            {
                case "ip":
                    return new IpProxyParser();
                case "socks5":
                    return new Sosks5ProxyParser();
                default:
                    throw new ArgumentException($"{parserString} is not supported");
            };
        }
    }

}
