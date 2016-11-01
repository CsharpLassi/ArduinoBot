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
            TelegramBotClient bot = new TelegramBotClient(token);

            CancellationTokenSource source = new CancellationTokenSource();

            bot.StartReceiving(source.Token);
            bot.OnMessage += (sender, e) =>
            {
                    if (e.Message.Text == "/start") 
                    {
                        bot.SendTextMessageAsync(e.Message.Chat.Id,string.Format("Hallo {0}",e.Message.From.FirstName));
                    }
                    else 
                    {
                        bot.SendTextMessageAsync(e.Message.Chat.Id,"Ich kann dich leider nicht verstehen");
                    }
            };

            Console.ReadLine();
            source.Cancel();

        }
    }
}

