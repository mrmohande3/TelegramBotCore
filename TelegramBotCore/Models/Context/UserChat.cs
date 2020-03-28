using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBotCore.Models.Context
{
    public class UserChat : Entity
    {
        [Key]
        public int UserChatId { get; set; }

        public int UserId { get; set; }
        public int ChatId { get; set; }
        public int UserTelegramId { get; set; }
        public int UserTelegramChatId { get; set; }
        public string InstaUserName { get; set; }
        public bool IsActive { get; set; }
        public virtual User User { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
