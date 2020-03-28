using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBotCore.Models.Bot
{
    public class MessageBot
    {
        public int message_id { get; set; }
        public UserBot from { get; set; }
        public int Date { get; set; }
        public ChatBot Chat { get; set; }
        public string text { get; set; }
    }
}
