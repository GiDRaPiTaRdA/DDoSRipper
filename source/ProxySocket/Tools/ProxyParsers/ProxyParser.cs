using System.Text.RegularExpressions;

namespace Tools.ProxyParsers
{
    public interface IProxyParser
    {
        Regex Regex { get; }

        ProxyData Parse(Match match);
    }
}