using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBotCore.Models.Context
{
    public class Crash: Entity
    {
        [Key]
        public int CrashId { get; set; }
        public int UserId { get; set; }
        public string MainUserName { get; set; }
        public string CrashUserName { get; set; }
        public virtual User User { get; set; }
    }
}
