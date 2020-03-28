using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramBotCore.Models.Bot;

namespace TelegramBotCore.Services
{
    public interface IBotService
    {
        void ReceiveMessage(UpdateBot updateBot);
    }
}
