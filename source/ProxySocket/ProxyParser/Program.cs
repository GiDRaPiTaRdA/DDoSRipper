using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tools;
using Console = Tools.Console;

namespace ProxyParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceFile = args[0];
            string mode = args[1];

            //if (mode == "1")
            //{
            //    List<string> prox = ProxyChecker.LoadProxies(sourceFile, regex1);

            //    prox.ForEach(Console.WriteLine);

            //    WriteDownToFile(Path.Combine(Path.GetDirectoryName(sourceFile), "export.txt"), prox.ToArray());
            //}
            //else if (mode == "2")
            //{
            //    string[] strings = File.ReadAllLines(sourceFile);


            //    List<string> proxies =
            //        strings
            //            .Select(s => regex2.Match(s))
            //            .Where(r => r.Success)
            //            .Select(s => $"socks5 {s.Groups[1]} {s.Groups[2]}")
            //            .ToList();

            //    WriteDownToFile(Path.Combine(Path.GetDirectoryName(sourceFile), "exportSOCKS5.txt"), proxies.ToArray());
            //}


        }

        private static void WriteDownToFile(string exportFile, string[] arr)
        {
            Console.WriteLine($"Export to {exportFile}");
            File.WriteAllLines(exportFile, arr);
        }
    }
}
