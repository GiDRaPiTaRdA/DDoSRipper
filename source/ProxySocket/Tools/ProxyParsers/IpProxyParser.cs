using System.Text.RegularExpressions;

namespace Tools.ProxyParsers
{
    public class IpProxyParser : IProxyParser
    {
        // 10.0.0.0:80
        public Regex Regex { get; } = new Regex("(\\d+\\.\\d+\\.\\d+\\.\\d+):(\\d+)");
        public ProxyData Parse(Match match)
        {
            return new ProxyData
            {
                Protocol = null,
                Ip = match.Groups[1].Value,
                Port = int.Parse(match.Groups[2].Value)
            };
        }
    }
}