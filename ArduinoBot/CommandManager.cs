using System;
using Telegram.Bot.Types;
using System.Collections.Generic;

namespace ArduinoBot
{
    

    public class CommandManager
    {
        public Dictionary<string,MessageEventHandler> Commands { get; private set; }

        public MessageEventHandler this[string index]
        {
            get
            {
                MessageEventHandler @out;
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
            Commands = new Dictionary<string, MessageEventHandler>();
        }
    }
}

