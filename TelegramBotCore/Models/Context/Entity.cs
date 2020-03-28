using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBotCore.Models.Context
{
    public class Entity
    {
        public bool IsRemoved { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime RemoveTime { get; set; }
    }
}
