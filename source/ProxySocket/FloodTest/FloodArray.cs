using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FloodTest
{
    public class FloodArray
    {
        public int Paralelism { get; set; }

        private const int timesToReconnect = 10;

        private Flooder[] Flooders { get; set; }

        public event EventHandler<string> LogEvent;

        public FloodArray()
        {
            this.Paralelism = Environment.ProcessorCount-2;
        }

        private void Log(string message) => this.LogEvent?.Invoke(this, message);

        public void Init(FloodConfig config)
        {
            this.Log($"Init flooders {this.Paralelism}");

            this.Flooders = new Flooder[this.Paralelism];

            try
            {
                for (int i = 0; i < this.Flooders.Length; i++)
                {
                    this.Flooders[i] = new Flooder(config);
                    
                    Console.WriteLine($"Connect Flooder {i}");
                    this.Flooders[i].TryConnect(timesToReconnect);
                    Console.WriteLine("Connected");
                }

                this.Log("Connect all success");
            }
            catch (Exception)
            {
                this.Log($"Connect {config.Ip}:{config.Port} failed");
            }

        }

        public void Send()
        {
             this.Flooders.First().Send();
        }

        public void Flood()
        {
            this.Log($"Start flood");

            foreach (Flooder flooder in this.Flooders)
            {
                Task.Factory.StartNew(flooder.Flood);
            }
        }

        public void Stop()
        {
            this.Log($"Stop flood");

            foreach (Flooder flooder in this.Flooders)
            {
                flooder.Stop();
            }
        }
    }
}