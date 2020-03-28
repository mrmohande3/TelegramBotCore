using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramBotCore.Models.Context;

namespace TelegramBotCore.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly BotContext _context;

        public RepositoryWrapper(BotContext context)
        {
            _context = context;
        }
        private RepositoryBase<Crash> crash;
        public RepositoryBase<Crash> Crashs
        {
            get
            {
                if (crash == null)
                    crash = new RepositoryBase<Crash>(_context);
                return crash;
            }
        }

        private RepositoryBase<User> users;
        public RepositoryBase<User> Users
        {
            get
            {
                if (users == null)
                    users = new RepositoryBase<User>(_context);
                return users;
            }
        }


        private RepositoryBase<Chat> chats;
        public RepositoryBase<Chat> Chats
        {
            get
            {
                if (chats == null)
                    chats = new RepositoryBase<Chat>(_context);
                return chats;
            }
        }
        private RepositoryBase<ChatMessage> chatMessages;
        public RepositoryBase<ChatMessage> ChatMessages
        {
            get
            {
                if (chatMessages == null)
                    chatMessages = new RepositoryBase<ChatMessage>(_context);
                return chatMessages;
            }
        }
        private RepositoryBase<UserChat> userChats;
        public RepositoryBase<UserChat> UserChats
        {
            get
            {
                if (userChats == null)
                    userChats = new RepositoryBase<UserChat>(_context);
                return userChats;
            }
        }




        //public void Dispose()
        //{
        //    _context?.Dispose();
        //}
    }
}
