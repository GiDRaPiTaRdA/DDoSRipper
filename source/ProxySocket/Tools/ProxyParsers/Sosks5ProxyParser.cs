using System.Text.RegularExpressions;

namespace Tools.ProxyParsers
{
    // ReSharper disable once IdentifierTypo
    public class Sosks5ProxyParser: IProxyParser
    {
        // socks5 10.0.0.0 80 
        public Regex Regex { get; } =  new Regex("^([a-zA-Z0-9]+) (\\d+\\.\\d+\\.\\d+\\.\\d+) (\\d+)$");
        public ProxyData Parse(Match match)
        {
            return new ProxyData
            {
                Protocol = match.Groups[1].Value,
                Ip = match.Groups[2].Value,
                Port = int.Parse(match.Groups[3].Value)
            };
        }
    }
}