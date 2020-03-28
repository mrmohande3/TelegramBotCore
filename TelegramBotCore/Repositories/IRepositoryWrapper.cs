using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramBotCore.Models.Context;

namespace TelegramBotCore.Repositories
{
    public interface IRepositoryWrapper 
    {
        RepositoryBase<User> Users { get; }
        RepositoryBase<Crash> Crashs { get; }
        RepositoryBase<Chat> Chats { get; }
        RepositoryBase<ChatMessage> ChatMessages { get; }
        RepositoryBase<UserChat> UserChats { get; }
    }
}
