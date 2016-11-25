using System;
using Telegram.Bot.Types;
using System.Text;

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
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("{0} V:{1}",board.Name,board.Version));
                foreach (var data in board.CurrentData)
                {
                    var name = Enum.GetName(typeof(DataID), data.Key);
                    sb.AppendLine(string.Format("->{0}={1}",name,data.Value.Value));
                }

                SendTextMessageAsync(message.User.ChatID,sb.ToString());

            }

        }

        protected override void OnDefault(MessageEventArgs message)
        {
            SendTextMessageAsync(message.User.ChatID,"Ich kann dich leider nicht verstehen");
        }
    }
}

