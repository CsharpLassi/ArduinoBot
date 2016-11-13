using System;
using Telegram.Bot;
using System.IO;
using System.Threading;
using System.IO.Ports;
using System.Linq;

namespace ArduinoBot
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var ports = SerialPort.GetPortNames().Where(i => i.StartsWith("/dev/ttyACM"));

            ArduinoManager manager = new ArduinoManager();
            foreach (var port in ports)
            {
                var board = manager.OpenArduino(port);
                Console.WriteLine("Board gefunden:{0}",board.Name);
            }

            var tokenpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".develop", "bottoken");
            var token = File.ReadAllText(tokenpath);
            token = token.Substring(0, token.Length - 1);
            ArduinoBot bot = new ArduinoBot(token,manager);

            CancellationTokenSource source = new CancellationTokenSource();

            bot.StartReceiving(source.Token);
            Console.WriteLine("Bot wurde gestartet.");
            Console.ReadLine();
            Console.WriteLine("Bot wird beendet");
            source.Cancel();
            Console.WriteLine("Bot wurde wurde beendet");
            Console.WriteLine("Boards werden geschlossen");
            manager.Close();
            Console.WriteLine("Boards wurden geschlossen");
            
        }
    }
}

