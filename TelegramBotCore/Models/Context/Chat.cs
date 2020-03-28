using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBotCore.Models.Context
{
    public class Chat : Entity
    {
        [Key]
        public int ChatId { get; set; }
        public string ChatUId { get; set; }
        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
        public virtual ICollection<UserChat> UserChats { get; set; }
    }
}
