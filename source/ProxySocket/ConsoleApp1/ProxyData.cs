namespace ConsoleApp1
{
    public struct ProxyData
    {
        public string Protocol { get; set; }

        public string Ip { get; set; }

        public string Port { get; set; }

        public override string ToString()
        {
            return $"{this.Protocol} {this.Ip}:{this.Port}";
        }
    }
}