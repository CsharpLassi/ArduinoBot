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

        private void OnStart(object sender,MessageEventArgs message)
        {
            SendTextMessageAsync(message.User.ChatID,string.Format("Hallo {0}",message.User.Username));
        }

        protected override void OnDefault(MessageEventArgs message)
        {
            SendTextMessageAsync(message.User.ChatID,"Ich kann dich leider nicht verstehen");
        }
    }
}

