using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class UnStatusService
    {
        private ITelegramBotClient _telegramBotClient = new TelegramBotClient("509847876:AAG0aTib65R2nDFZmzu7zCbRydHBJ8MpuwI");
        private Models.Context.User user;
        private IUtilitiesWrapper _utilitiesWrapper;
        private IRepositoryWrapper _repositoryWrapper;

        public UnStatusService(IUtilitiesWrapper utilitiesWrapper, IRepositoryWrapper repositoryWrapper)
        {
            _utilitiesWrapper = utilitiesWrapper;
            _repositoryWrapper = repositoryWrapper;
        }
        public async void CheckMessage(UpdateBot updateBot)
        {
            user = _repositoryWrapper.Users.Where(u => u.ChatId == updateBot.message.Chat.id).FirstOrDefault();
            switch (user.UserStatus)
            {
                case UserStatus.SignUp:
                    GiveUserName(updateBot);
                    break;
                case UserStatus.GiveCrashes:
                    GiveUserCrashes(updateBot);
                    break;
                default:
                    SetMain(updateBot.message.Chat.id);
                    break;
            }

        }

        private async void GiveUserCrashes(UpdateBot updateBot)
        {
            var usernames = updateBot.message.text.Split('\n');

            foreach (var username in usernames)
            {
                var crash = await _repositoryWrapper.Users.GetEntryAsync(u => u.InstaUserName == username);
                if (crash != null)
                {
                    await _telegramBotClient.SendTextMessageAsync(crash.ChatId,
                        $"یک نفر ای دی شما را به لیست کراش های خود افزود");

                }

                var cc = await _repositoryWrapper.Crashs.GetEntryAsync(u => u.UserId == user.UserId && u.CrashUserName == username);
                if ( cc == null)
                {

                    await _repositoryWrapper.Crashs.AddEntryAsync(new Crash
                    {
                        UserId = user.UserId,
                        MainUserName = user.InstaUserName,
                        CrashUserName = username
                    });
                }
            }
            user.UserStatus = UserStatus.NULL;
            await _repositoryWrapper.Users.UpdateEntryAsync(user);
            await _telegramBotClient.SendTextMessageAsync(user.ChatId,
                $"لیست کراش های شما بروز شد حال از امکانات کامل ربات استفاده نمایید", ParseMode.Default, false, false, 0,
                _utilitiesWrapper.KeyboardFactory.GetKeyboard(KeyboardType.MainMenu));

        }

        private async void GiveUserName(UpdateBot updateBot)
        {
            user.UserStatus = UserStatus.GiveUserName;
            if (updateBot.message.text.Contains('@'))
            {
                user.UserStatus = UserStatus.GiveUserName;
                user.InstaUserName = updateBot.message.text;
                await _repositoryWrapper.Users.UpdateEntryAsync(user);
                var userUrl = _utilitiesWrapper.Utility.GetUserLink(user.UserUid);
                await _telegramBotClient.SendTextMessageAsync(user.ChatId,
                    $"لطفا پیج زیر را دنبال کرده و دوباره دکمه اعتبار سنجی را بفشارید \n @mr.mohande3", ParseMode.Default, false, false, 0,
                    new InlineKeyboardMarkup(new InlineKeyboardButton
                    {
                        Text = "ورود به پیج",
                        Url = $"https://www.instagram.com/mr.mohande3"
                    }));
                await _telegramBotClient.SendTextMessageAsync(user.ChatId,
                    $"بعد از فالو کردن پیج اعتبار سنجی شما کامل می شود ، این اعتبار سنجی تنها برای حفظ اطلاعات شخمی شماست", ParseMode.Default, false, false, 0,
                    _utilitiesWrapper.KeyboardFactory.GetKeyboard(KeyboardType.GiveUserName));
            }
            else
                await _telegramBotClient.SendTextMessageAsync(user.ChatId,
                    $"لطفا ای دی اینستاگرام را به درستی وارد کنید مثال : @HHHHH");

        }

        private async void SetMain(int chatid)
        {
            if (user.IsVerified)
                 await _telegramBotClient.SendTextMessageAsync(chatid,
                    "صفحه اصلی", ParseMode.Default, false, false, 0,
                    _utilitiesWrapper.KeyboardFactory.GetKeyboard(KeyboardType.MainMenu));
            else
            {
                user.UserStatus = UserStatus.SignUp;
                _repositoryWrapper.Users.UpdateEntry(user);
                await _telegramBotClient.SendTextMessageAsync(chatid,
                    " لطفا ای دی اینستاگرام خود را واردی کنید");
                 await _telegramBotClient.SendTextMessageAsync(chatid,
                    "مثال : @HHHHH", ParseMode.Default, false, false, 0,
                    _utilitiesWrapper.KeyboardFactory.GetKeyboard(KeyboardType.Back));
            }
        }
    }
}
