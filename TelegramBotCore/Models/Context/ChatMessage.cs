using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBotCore.Models.Context
{
    public class ChatMessage : Entity
    {
        [Key]
        public int ChatMessageId { get; set; }
        public int SenderChatId { get; set; }
        public string SenderName { get; set; }
        public int ReceiverChatId { get; set; }
        public string ReceiverName { get; set; }
        public string Text { get; set; }
        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
