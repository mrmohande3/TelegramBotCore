using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBotCore.Models.Context
{
    public class ChannalJioned : Entity
    {
        [Key]
        public int JionedId { get; set; }
        public string ChannalId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
