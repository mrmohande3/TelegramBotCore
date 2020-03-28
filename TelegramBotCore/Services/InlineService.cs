using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotCore.Models.Bot;
using TelegramBotCore.Repositories;
using TelegramBotCore.Utilities;

namespace TelegramBotCore.Services
{
    public class InlineService
    {
        private ITelegramBotClient _telegramBotClient = new TelegramBotClient(Startup.CrushBot);
        private ITelegramBotClient _telegramUnknownBotClient = new TelegramBotClient(Startup.UnknownBot);
        private Models.Context.User user;
        private readonly IUtilitiesWrapper _utilitiesWrapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public InlineService(IUtilitiesWrapper utilitiesWrapper, IRepositoryWrapper repositoryWrapper)
        {
            _utilitiesWrapper = utilitiesWrapper;
            _repositoryWrapper = repositoryWrapper;
        }
        public async void CheckInline(CallBackQueryBot updateBotCallQuery)
        {
            user = await _repositoryWrapper.Users.GetEntryAsync(u => u.ChatId == updateBotCallQuery.message.Chat.id); 
            if (updateBotCallQuery.data.Contains("Remove"))
            {
                if (updateBotCallQuery.data.Contains("Crush"))
                    RemoveCrush(int.Parse(updateBotCallQuery.data.Split("_").Last()));
            }
            else if (updateBotCallQuery.data.Contains("Show"))
            {
                if (updateBotCallQuery.data.Contains("CrushIAm"))
                    ShowCrushIAm();
            }else if (updateBotCallQuery.data.Contains("Active"))
            {
                if (updateBotCallQuery.data.Contains("Chat"))
                    ActiveChat(int.Parse(updateBotCallQuery.data.Split("_").Last()));
            }
        }

        private async void ActiveChat(int chatId)
        {
            var uChat =  await _repositoryWrapper.UserChats.GetEntryAsync(c => c.ChatId == chatId && c.UserId==user.UserId);
            var chat = await _repositoryWrapper.Chats.GetEntryAsync(uChat.ChatId);
            var userChats = await _repositoryWrapper.UserChats.WhereAsync(uc => uc.ChatId == chat.ChatId);
            if (!uChat.IsActive)
            {
                var cc = await _repositoryWrapper.UserChats.WhereAsync(c => c.UserId == uChat.UserId);
                foreach (var userChat in cc)
                {
                    if (userChat.IsActive)
                    {
                        userChat.IsActive = false;
                        await _repositoryWrapper.UserChats.UpdateEntryAsync(userChat);
                    }
                }
                uChat.IsActive = true;
                await _repositoryWrapper.UserChats.UpdateEntryAsync(uChat);
            }

            bool flag = false;
            foreach (var userchat in userChats)
            {
                if (userchat.IsActive)
                    flag = true;
                else
                    flag = false;
            }

            if (flag)
            {
                foreach (var userChat in userChats)
                {
                    await _telegramBotClient.SendTextMessageAsync(userChat.UserTelegramChatId, "برای استفاده از چت ناشناس از ربات @GeustTimeBot استفاده کنید");
                    await _telegramUnknownBotClient.SendTextMessageAsync(userChat.UserTelegramChatId, "هر دو طرف اماده چت هستند ، لذت ببرید");
                }
            }
            else
            {
                await _telegramBotClient.SendTextMessageAsync(user.ChatId, "هنوز طرف دیگر اماده چت نیست");
            }
        }

        private async void ShowCrushIAm()
        {
            List<string> CahnnalList = new List<string>()
            {
                "@testbotformy"
            };
            List<string> UnJoined = new List<string>();
            foreach (var channal in CahnnalList)
            {
                try
                {
                    var tchat = await _telegramBotClient.GetChatMemberAsync(channal, user.TelegramUserId);
                    if(tchat.Status==ChatMemberStatus.Left || tchat.Status==ChatMemberStatus.Kicked || tchat.Status==ChatMemberStatus.Restricted)
                        UnJoined.Add(channal);
                }
                catch (Exception e)
                {
                    UnJoined.Add(channal);
                }
            }
            if (UnJoined.Count==0)
            {
                foreach (var crash in _repositoryWrapper.Crashs.Where(u => u.CrashUserName == user.InstaUserName))
                {
                    await _telegramBotClient.SendTextMessageAsync(user.ChatId, crash.MainUserName, ParseMode.Default, false, false, 0,
                        new InlineKeyboardMarkup(new List<InlineKeyboardButton>
                        {
                            new InlineKeyboardButton{Text = "چت ناشناس",CallbackData = $"ChatUnknown_Crush_{crash.MainUserName}"}
                        }));
                }
            }
            else
            {
                await _telegramBotClient.SendTextMessageAsync(user.ChatId, $"لطفا در لیست کانال های ما عضو شوید و دوباره روی نمایش کلیک کنید");
                foreach (var channal in UnJoined)
                {

                    await _telegramBotClient.SendTextMessageAsync(user.ChatId, $"{channal}");
                }
                
            }
        }

        private async void RemoveCrush(int crushId)
        {
            var crush = await _repositoryWrapper.Crashs.GetEntryAsync(crushId);
            if (crush != null)
            {
                await _repositoryWrapper.Crashs.RemoveEntryAsync(crush);
                await _telegramBotClient.SendTextMessageAsync(user.ChatId,$"ای دی {crush.CrashUserName} از لیست شما حذف شد");
            }
            else
            {
                await _telegramBotClient.SendTextMessageAsync(user.ChatId, $"ای دی ارسال صحیح نیست");

            }
        }
    }
}
