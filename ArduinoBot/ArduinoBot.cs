using System;
using Telegram.Bot.Types;

namespace ArduinoBot
{
    public class ArduinoBot : BaseCommandBot
    {
        public ArduinoManager ArduinoManager { get; private set; }

        public ArduinoBot(string token,ArduinoManager manager) : base(token)
        {
            ArduinoManager = manager;

            CommandManager["start"] += OnStart;
            CommandManager["ls"] += OnLs;
        }

        private void OnStart(object sender,MessageEventArgs message)
        {
            SendTextMessageAsync(message.User.ChatID,string.Format("Hallo {0}",message.User.Username));
        }

        private void OnLs(object sender,MessageEventArgs message)
        {
            SendTextMessageAsync(message.User.ChatID,"Aktuell verbunden ist:");
            foreach (var board in ArduinoManager.Boards)
            {
                SendTextMessageAsync(message.User.ChatID,string.Format("{0} V:{1}",board.Name,board.Version));
            }

        }

        protected override void OnDefault(MessageEventArgs message)
        {
            SendTextMessageAsync(message.User.ChatID,"Ich kann dich leider nicht verstehen");
        }
    }
}

