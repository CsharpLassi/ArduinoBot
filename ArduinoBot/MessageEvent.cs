using System;
using Telegram.Bot.Types;

namespace ArduinoBot
{
    public delegate void MessageEventHandler(object sender,MessageEventArgs e);

    public class MessageEventArgs
    {
        public Message Message { get; private set; }
        public BotUser User { get; private set; }

        public MessageEventArgs(Message message,BotUser user)
        {
            Message = message;
            User = user;
        }
    }
}

