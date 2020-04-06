using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnumsNET;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotCore.Models;
using TelegramBotCore.Models.Bot;
using TelegramBotCore.Models.Context;
using TelegramBotCore.Repositories;
using TelegramBotCore.Utilities;

namespace TelegramBotCore.Services
{
    public class UnknownBot : IBotService
    {
        private ITelegramBotClient _telegramUnkownBotClient = new TelegramBotClient(Startup.UnknownBot);
        private Models.Context.User user;
        private IUtilitiesWrapper _utilitiesWrapper;
        private IRepositoryWrapper _repositoryWrapper;

        public UnknownBot(IUtilitiesWrapper utilitiesWrapper, IRepositoryWrapper repositoryWrapper)
        {
            _utilitiesWrapper = utilitiesWrapper;
            _repositoryWrapper = repositoryWrapper;
        }
        public async void SendMessage(string message)
        {
            UserChat curretnSide = await _repositoryWrapper.UserChats.GetEntryAsync(uc => uc.UserId == user.UserId && uc.IsActive);
            UserChat otheSide = await _repositoryWrapper.UserChats.GetEntryAsync(uc => uc.ChatId == curretnSide.ChatId && uc.IsActive && uc.UserId != user.UserId);
            ChatMessage chatMessage = new ChatMessage
            {
                Text = message,
                ChatId = curretnSide.ChatId,
                SenderChatId = curretnSide.UserTelegramChatId,
                ReceiverChatId = otheSide.UserTelegramChatId,
                SenderName = curretnSide.InstaUserName,
                ReceiverName = otheSide.InstaUserName
            };
            _repositoryWrapper.ChatMessages.AddEntry(chatMessage);
            if (message != null)
                await _telegramUnkownBotClient.SendTextMessageAsync(otheSide.UserTelegramChatId, message, ParseMode.Default, false, false, 0, new ReplyKeyboardMarkup(new KeyboardButton("بستن و قطع چت")));
        }

        public async void ReceiveMessage(UpdateBot updateBot)
        {
            string Message = updateBot.message.text;
            user = await _repositoryWrapper.Users.GetEntryAsync(u => u.ChatId == updateBot.message.Chat.id);
            if ((UserStatus.SignUp).AsString(EnumFormat.Description) == Message)
                CloseChat();
            else
                SendMessage(Message);
        }

        private void CloseChat()
        {
            
        }
    }
}
