using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public static class Console
    {
        public static bool AnsiColorMode { get; set; }

        public static void Write(string message, ConsoleColor color)
        {
            if (AnsiColorMode)
            {
                System.Console.Write(AnsiString(message, color));
            }
            else
            {
                CustomConsole.Console.Write(message, color);
            }
        }

        public static void WriteLine(string message)=>
            System.Console.WriteLine(message);

        public static string ReadLine() =>  
            System.Console.ReadLine();

        public static void WriteLine(string message, ConsoleColor color)
        {
            if (AnsiColorMode)
            {
                System.Console.WriteLine(AnsiString(message, color));
            }
            else
            {
                CustomConsole.Console.WriteLine(message, color);
            }
        }



        static string AnsiString(string message, ConsoleColor color) =>
            $"{Colors[color]}{message}{Reset}";


        private static Dictionary<ConsoleColor, string> Colors { get; } = new Dictionary<ConsoleColor, string>()
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

        private static string Reset => "\u001b[0m";
    }


}
