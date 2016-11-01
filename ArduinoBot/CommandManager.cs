using System;
using Telegram.Bot.Types;
using System.Collections.Generic;

namespace ArduinoBot
{
    public class CommandManager
    {
        public Dictionary<string,Action<Message>> Commands { get; private set; }

        public Action<Message> this[string index]
        {
            get
            {
                Action<Message> @out;
                Commands.TryGetValue(index,out @out);
                return @out;
            }
            set
            {
                if (Commands.ContainsKey(index))
                    Commands[index] = value;
                else
                    Commands.Add(index, value);
            }
        }

        public CommandManager()
        {
            Commands = new Dictionary<string, Action<Message>>();
        }
    }
}

