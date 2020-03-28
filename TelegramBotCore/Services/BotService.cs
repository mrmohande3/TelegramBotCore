using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBotCore.Models;
using TelegramBotCore.Models.Bot;
using TelegramBotCore.Models.Context;
using TelegramBotCore.Repositories;
using TelegramBotCore.Utilities;

namespace TelegramBotCore.Services
{
    public class BotService : IBotService
    {
        private StatusService _statusService;
        private UnStatusService _unStatusService;
        private InlineService _inlineService;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private ITelegramBotClient _telegramBotClient = new TelegramBotClient("509847876:AAG0aTib65R2nDFZmzu7zCbRydHBJ8MpuwI");
        private IUtilitiesWrapper _utilitiesWrapper;
        public BotService(InlineService inlineService,StatusService statusService, IRepositoryWrapper repositoryWrapper,UnStatusService unStatusService , IUtilitiesWrapper utilitiesWrapper)
        {
            _utilitiesWrapper = utilitiesWrapper;
            _statusService = statusService;
            _repositoryWrapper = repositoryWrapper;
            _unStatusService = unStatusService;
            _inlineService = inlineService;
        }


        public async void ReceiveMessage(UpdateBot updateBot)
        {
            if (updateBot.message != null)
            {
                var user = _repositoryWrapper.Users.GetEntry(u => u.ChatId==updateBot.message.Chat.id);
                if (user == null)
                {
                    _repositoryWrapper.Users.AddEntry(new Models.Context.User
                    {
                        UserName = updateBot.message.from.username,
                        FirstName = updateBot.message.from.first_name,
                        LastName = updateBot.message.from.last_name,
                        ChatId = updateBot.message.Chat.id,
                        TelegramUserId = updateBot.message.from.id,
                        UserUid = _utilitiesWrapper.Utility.GetUid()
                    });
                }
                if (_statusService.IsStatus(updateBot.message.text) != UserStatus.NULL)
                {
                    _statusService.SendStatus(_statusService.IsStatus(updateBot.message.text), updateBot);
                }
                else
                {
                    _unStatusService.CheckMessage(updateBot);
                }
            }

            if (updateBot.callback_query != null)
            {
                _inlineService.CheckInline(updateBot.callback_query);
            }
        }

        public void SendMessage(string message, string chatId)
        {
            _telegramBotClient.SendTextMessageAsync(chatId, message);
        }


    }
}
