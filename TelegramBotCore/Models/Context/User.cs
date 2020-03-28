using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBotCore.Models.Context
{
    public class User: Entity
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string InstaUserName { get; set; }
        public UserStatus UserStatus { get; set; }
        public string PhoneNumber { get; set; }
        public string UserUid { get; set; }
        public int ChatId { get; set; }
        public bool IsVerified { get; set; }
        public int TelegramUserId { get; set; }
        public int UnknownChatId { get; set; }
        public string InstaUserId { get; set; }
        public string InstaProfileImage { get; set; }

        public virtual ICollection<ChannalJioned> Jioneds { get; set; }
        public virtual ICollection<Crash> Crashes { get; set; }
    }
}
