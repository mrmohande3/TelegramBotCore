using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TelegramBotCore.Models.Context;

namespace TelegramBotCore.Services
{
    public class DBInitializer
    {
        private readonly BotContext _context;
        public DBInitializer(BotContext context)
        {
            _context = context;
        }
        public void Seed()
        {

            _context.Database.Migrate();
        }
    }
}
