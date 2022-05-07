using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            foreach (ConsoleColor color in Colors.Select(c=>c.Key))
            {
                WriteLine($"Hello World {color}", color);
            }
        }

        static void WriteLine(string message, ConsoleColor color)=>
            WriteLine(message, Colors[color]);
        

        static void WriteLine(string message, string color)=>
            Console.WriteLine($"{color}{message}{Reset}");
        

        public static Dictionary<ConsoleColor, string> Colors { get; } = new Dictionary<ConsoleColor, string>()
        {
            {ConsoleColor.Black,"\u001b[30m" },
            {ConsoleColor.Red,  "\u001b[31m" },
            {ConsoleColor.DarkRed,  "\u001b[31m" },
            {ConsoleColor.Green,"\u001b[32m"},
            {ConsoleColor.DarkGreen,"\u001b[32m"},
            {ConsoleColor.Yellow,"\u001b[33m"},
            {ConsoleColor.DarkYellow,"\u001b[33m"},
            {ConsoleColor.Blue,"\u001b[34m"},
            {ConsoleColor.DarkBlue,"\u001b[34m"},
            {ConsoleColor.Magenta,"\u001b[35m"},
            {ConsoleColor.DarkMagenta,"\u001b[35m"},
            {ConsoleColor.Cyan,"\u001b[36m"},
            {ConsoleColor.DarkCyan,"\u001b[36m"},
            {ConsoleColor.White,"\u001b[37m"}
        };

        static string Reset => "\u001b[0m";
    }
}
