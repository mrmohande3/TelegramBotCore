using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramBotCore.Repositories;

namespace TelegramBotCore.Utilities
{
    public class UtilitiesWrapper : IUtilitiesWrapper
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UtilitiesWrapper(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public KeyboardFactory KeyboardFactory
        {
            get { return new KeyboardFactory(); }
        }

        public Utility Utility
        {
            get { return new Utility(_repositoryWrapper); }
        }
    }
}
