using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using System.Collections.Generic;

namespace ArduinoBot
{
    public abstract class BaseCommandBot : TelegramBotClient
    {
        public CommandManager CommandManager { get; private set; }

        public BotUserManager UserManager { get; private set; }


        public BaseCommandBot(string token) : base(token)
        {
            CommandManager = new CommandManager();
            UserManager = new BotUserManager();
        }



        protected override void OnUpdateReceived(UpdateEventArgs e)
        {
            if (e.Update.Message != null)
            {
                OnMessage(e.Update.Message);
            }
        }

        private void OnMessage(Message e)
        {
            var user = UserManager.GetUser(e.From, e.Chat.Id);
            var eventargs = new MessageEventArgs(e, user);

            if (e.Text.StartsWith("/"))
            {
                var command = e.Text.Remove(0, 1);
                if (CommandManager.Commands.ContainsKey(command))
                {


                    CommandManager.Commands[command].Invoke(this,eventargs);
                    return;
                }
            }
            OnDefault(eventargs);
        }

        protected virtual void OnDefault(MessageEventArgs message)
        {
        }
    }
}

