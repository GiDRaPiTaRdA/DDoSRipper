namespace Tools
{
    public class ProxyData
    {
        public string Protocol { get; set; }

        public string Ip { get; set; }

        public int Port { get; set; }

        public override string ToString()
        {
            return $"{this.Protocol} {this.Ip}:{this.Port}";
        }
    }
}