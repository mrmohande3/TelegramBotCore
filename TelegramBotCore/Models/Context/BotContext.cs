using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TelegramBotCore.Models.Context
{
    public class BotContext:DbContext
    {
        public BotContext(DbContextOptions<BotContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Crash> Crashes { get; set; }
        public DbSet<ChannalJioned> ChannalJioneds { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

    }
}
