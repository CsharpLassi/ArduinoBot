using System;
using Telegram.Bot;
using System.IO;
using System.Threading;

namespace ArduinoBot
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var tokenpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".develop", "bottoken");

            var token = File.ReadAllText(tokenpath);
            token = token.Substring(0, token.Length - 1);
            ArduinoBot bot = new ArduinoBot(token);

            CancellationTokenSource source = new CancellationTokenSource();

            bot.StartReceiving(source.Token);
            Console.WriteLine("Bot wurde gestartet.");
            Console.ReadLine();
            Console.WriteLine("Bot wird beendet");
            source.Cancel();

        }
    }
}

