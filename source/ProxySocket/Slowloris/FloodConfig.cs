namespace Slowloris
{
    public struct FloodConfig
    {
        public string Ip { get; set; }
        public int Port { get; set; }

        public bool Blocking { get; set; }
        public bool NoDelay { get; set; }

        public int Delay { get; set; }
    }
}