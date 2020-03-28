using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBotCore.Models.Bot
{
    public class UpdateBot
    {
        public int update_id { get; set; }
        public MessageBot message { get; set; }
        public MessageBot edited_message { get; set; }
        public MessageBot channel_post { get; set; }
        public MessageBot edited_channel_post { get; set; }
        public InlineQueryBot inline_query { get; set; }
        public CallBackQueryBot callback_query { get; set; }
    }
}
