using System;
using System.Xml;

namespace ArduinoBot
{
    [Serializable]
    public class BotUser
    {
        public long UserID { get; set; }

        public long ChatID { get; set; }

        public string Username { get; set; }

        public BotUser()
        {
        }
    }
}

