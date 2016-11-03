using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;

namespace ArduinoBot
{
    public class BotUserManager
    {

        private Dictionary<long,BotUser> _userlist = new Dictionary<long, BotUser>();
        public IEnumerable<BotUser> SessionUserList { get { return _userlist.Select ( i => i.Value); }}

        public BotUserManager()
        {
        }
            
        public BotUser GetUser(User user,long chatid)
        {
            if (_userlist.ContainsKey(user.Id))
            {
                return _userlist[user.Id];
            }
            else
            {
                var name = user.Username ?? user.FirstName;
                BotUser newuser = new BotUser()
                {
                    UserID = user.Id,
                    ChatID = chatid,
                    Username = name,
                };

                _userlist.Add(user.Id, newuser);

                Console.WriteLine("Neuer User angelegt: {0}",name);

                return newuser;
            }
        }
    }
}

