using System;
using Telegram.Bot.Types;

namespace ArduinoBot
{
    public class ArduinoBot : BaseCommandBot
    {
        

        public ArduinoBot(string token) : base(token)
        {
            CommandManager["start"] += OnStart;
        }

        private void OnStart(Message message)
        {
            SendTextMessageAsync(message.Chat.Id,string.Format("Hallo {0}",message.From.FirstName));
        }

        protected override void OnDefault(Message message)
        {
            SendTextMessageAsync(message.Chat.Id,"Ich kann dich leider nicht verstehen");
        }
    }
}

