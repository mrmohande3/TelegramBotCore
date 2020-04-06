using EnumsNET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotCore.Models;
using TelegramBotCore.Models.Bot;
using TelegramBotCore.Models.Context;
using TelegramBotCore.Repositories;
using TelegramBotCore.Utilities;
using Chat = TelegramBotCore.Models.Context.Chat;
using User = Telegram.Bot.Types.User;

namespace TelegramBotCore.Services
{
    public class StatusService
    {
        private ITelegramBotClient _telegramBotClient = new TelegramBotClient("509847876:AAG0aTib65R2nDFZmzu7zCbRydHBJ8MpuwI");
        private Models.Context.User user;
        private IUtilitiesWrapper _utilitiesWrapper;
        private IRepositoryWrapper _repositoryWrapper;

        public StatusService(IUtilitiesWrapper utilitiesWrapper, IRepositoryWrapper repositoryWrapper)
        {
            _utilitiesWrapper = utilitiesWrapper;
            _repositoryWrapper = repositoryWrapper;
        }
        public UserStatus IsStatus(string message)
        {
            if ((UserStatus.SignUp).AsString(EnumFormat.Description) == message)
                return UserStatus.SignUp;
            else if ((UserStatus.Start).AsString(EnumFormat.Description) == message)
                return UserStatus.Start;
            else if ((UserStatus.Back).AsString(EnumFormat.Description) == message)
                return UserStatus.Back;
            else if ((UserStatus.GiveUserName).AsString(EnumFormat.Description) == message)
                return UserStatus.GiveUserName;
            else if ((UserStatus.HowManyCrushIAm).AsString(EnumFormat.Description) == message)
                return UserStatus.HowManyCrushIAm;
            else if ((UserStatus.CrushList).AsString(EnumFormat.Description) == message)
                return UserStatus.CrushList;
            else if ((UserStatus.Profile).AsString(EnumFormat.Description) == message)
                return UserStatus.CrushList;
            else if ((UserStatus.GiveCrashes).AsString(EnumFormat.Description) == message)
                return UserStatus.GiveCrashes;
            else if ((UserStatus.ChatUnknown).AsString(EnumFormat.Description) == message)
                return UserStatus.ChatUnknown;
            else if ((UserStatus.UnicornPic).AsString(EnumFormat.Description) == message)
                return UserStatus.UnicornPic;
            else
                return UserStatus.NULL;
        }

        public void SendStatus(UserStatus status, UpdateBot updateBot)
        {
            user = _repositoryWrapper.Users.Where(u => u.ChatId == updateBot.message.Chat.id).FirstOrDefault();
            switch (status)
            {
                case UserStatus.Start:
                    SetMain(updateBot.message.Chat.id);
                    break;
                case UserStatus.SignUp:
                    SignUpStatus(updateBot);
                    break;
                case UserStatus.GiveUserName:
                    VerifiedUserName(updateBot);
                    break;
                case UserStatus.HowManyCrushIAm:
                    HowManyCrushIAm(updateBot);
                    break;
                case UserStatus.CrushList:
                    CrushList(updateBot);
                    break;
                case UserStatus.GiveCrashes:
                    GiveCrushUser(updateBot);
                    break;
                case UserStatus.ChatUnknown:
                    ChatUnknown(updateBot);
                    break;
                case UserStatus.UnicornPic:
                    GetPicture();
                    break;
                default:
                    SetMain(updateBot.message.Chat.id);
                    break;
            }
        }

        private async void GetPicture()
        {
            var crushCount = (await _repositoryWrapper.Crashs.WhereAsync(c => c.CrashUserName == user.InstaUserName)).Count();
            if (user.InstaProfileImage == null)
            {
                RestClient client = new RestClient();
                RestRequest request = new RestRequest($"https://www.instagram.com/{user.InstaUserName.Substring(1)}/?__a=1", Method.GET);
                var res = await client.ExecuteGetAsync<InstaRoot>(request);
                if (res.IsSuccessful)
                {
                    var profilePhoto = res.Data.graphql.user.profile_pic_url_hd;
                    user.InstaProfileImage = profilePhoto;
                    user = _repositoryWrapper.Users.UpdateEntry(user);
                }
            }

            RestClient getPictureClient = new RestClient();
            RestRequest postRequest = new RestRequest("http://127.0.0.1:5000/image", Method.POST);
            postRequest.AddParameter("url", user.InstaProfileImage);
            postRequest.AddParameter("crushes", crushCount.ToString());
            postRequest.AddParameter("id", user.InstaUserName.Substring(1));
            var resPhoto = getPictureClient.DownloadData(postRequest);
            if (resPhoto != null)
            {
                using (MemoryStream memory = new MemoryStream(resPhoto))
                {
                    var file = new InputMedia(memory, "عکس اختصاصی شما");
                    try
                    {
                        await _telegramBotClient.SendPhotoAsync(user.ChatId, file);
                    }
                    catch (Exception e)
                    {
                        GetPicture();
                    }
                }
            }

        }

        private async void ChatUnknown(UpdateBot updateBot)
        {
            var chats = await _repositoryWrapper.UserChats.WhereAsync(u => u.UserId == user.UserId);
            if (chats.Count() > 0)
            {
                foreach (var userChat in chats)
                {

                    await _telegramBotClient.SendTextMessageAsync(user.ChatId, "چت ناشناس که قبلا متصل بودید", ParseMode.Default, false, false, 0,
                        new InlineKeyboardMarkup(new List<InlineKeyboardButton>
                        {
                            new InlineKeyboardButton{Text = "شروع چت",CallbackData = $"Active_Chat_{userChat.ChatId}" }
                        }));
                }
            }
            else
            {
                var crushes = await _repositoryWrapper.Crashs.WhereAsync(c => c.CrashUserName == user.InstaUserName);
                if (crushes.Count() > 0)
                {
                    var rand = new Random(DateTime.Now.Millisecond);
                    var crush = await _repositoryWrapper.Users.GetEntryAsync(u => u.UserId == crushes.ToList()[rand.Next(0, crushes.Count() - 1)].UserId);
                    var chat = await _repositoryWrapper.Chats.AddEntryAsync(new Chat
                    {
                        ChatUId = Guid.NewGuid().ToString("N").Substring(0, 9)
                    });
                    await _repositoryWrapper.UserChats.AddEntryAsync(new UserChat
                    {
                        ChatId = chat.ChatId,
                        UserId = user.UserId,
                        UserTelegramChatId = user.ChatId,
                        UserTelegramId = user.TelegramUserId,
                        InstaUserName = user.InstaUserName
                    });
                    await _repositoryWrapper.UserChats.AddEntryAsync(new UserChat
                    {
                        ChatId = chat.ChatId,
                        UserId = crush.UserId,
                        UserTelegramChatId = crush.ChatId,
                        UserTelegramId = crush.TelegramUserId,
                        InstaUserName = crush.InstaUserName
                    });

                    await _telegramBotClient.SendTextMessageAsync(crush.ChatId, "کراش شما چت ناشناسی را با شما اغاز کرد", ParseMode.Default, false, false, 0,
                        new InlineKeyboardMarkup(new List<InlineKeyboardButton>
                        {
                            new InlineKeyboardButton{Text = "شروع چت",CallbackData = $"Active_Chat_{chat.ChatId}"}
                        }));
                    await _telegramBotClient.SendTextMessageAsync(user.ChatId, "ناشناس با کراش", ParseMode.Default, false, false, 0,
                        new InlineKeyboardMarkup(new List<InlineKeyboardButton>
                        {
                            new InlineKeyboardButton{Text = "شروع چت",CallbackData = $"Active_Chat_{chat.ChatId}"}
                        }));
                }
            }

        }
        private async void GiveCrushUser(UpdateBot updateBot)
        {
            user.UserStatus = UserStatus.GiveCrashes;
            await _telegramBotClient.SendTextMessageAsync(user.ChatId,
               $"کاربر گرامی با موفقیت اعتبار سنجی شما انجام شد \n حال لیست کراش های خود را وارد کنید");
            await _telegramBotClient.SendTextMessageAsync(user.ChatId,
               $"ای دی تلگرام انها را در هر خط وارد کنید \n مثال : \n @HHHHH \n @SSSSS", ParseMode.Default, false, false, 0,
               _utilitiesWrapper.KeyboardFactory.GetKeyboard(KeyboardType.Back));
            await _repositoryWrapper.Users.UpdateEntryAsync(user);
        }
        private async void CrushList(UpdateBot updateBot)
        {
            foreach (var crash in await _repositoryWrapper.Crashs.WhereAsync(u => u.UserId == user.UserId))
            {
                await _telegramBotClient.SendTextMessageAsync(user.ChatId, crash.CrashUserName, ParseMode.Default, false, false, 0,
                   new InlineKeyboardMarkup(new List<InlineKeyboardButton>
                   {
                        new InlineKeyboardButton{Text = "حذف",CallbackData = $"Remove_Crush_{crash.CrashId}"}
                   }));
            }
            await _telegramBotClient.SendTextMessageAsync(user.ChatId, "شما می توانید لیست کراش های خود را ادیت نمایید", ParseMode.Default, false, false, 0,
               new ReplyKeyboardMarkup(new List<IEnumerable<KeyboardButton>>
               {
                    new []{new KeyboardButton("افزودن کراش"), },
                    new []{new KeyboardButton("بازگشت"), },
               }));
        }
        private async void HowManyCrushIAm(UpdateBot updateBot)
        {
            var crusheIAm = (await _repositoryWrapper.Crashs.WhereAsync(u => u.CrashUserName == user.InstaUserName)).Count();
            if (crusheIAm > 0)
            {
                await _telegramBotClient.SendTextMessageAsync(user.ChatId,
                   $"شما کراش {crusheIAm}نفر هستید", ParseMode.Default, false, false, 0,
                   new InlineKeyboardMarkup(new InlineKeyboardButton
                   {
                       Text = "نمایش انها",
                       CallbackData = "Show_CrushIAm"
                   }));
            }
            else
            {

                await _telegramBotClient.SendTextMessageAsync(user.ChatId,
                   "شما تا به حال کراش کسی نشده اید", ParseMode.Default, false, false, 0,
                   _utilitiesWrapper.KeyboardFactory.GetKeyboard(KeyboardType.MainMenu));
            }
        }
        private async void VerifiedUserName(UpdateBot updateBot)
        {
            var flag = await _utilitiesWrapper.Utility.CheckInstagramVerified(user);
            if (flag)
            {
                var userUrl = _utilitiesWrapper.Utility.GetUserLink(user.UserUid);
                user.UserStatus = UserStatus.GiveCrashes;
                user.IsVerified = true;
                await _repositoryWrapper.Users.UpdateEntryAsync(user);
                await _telegramBotClient.SendTextMessageAsync(user.ChatId,
                   $"کاربر گرامی با موفقیت اعتبار سنجی شما انجام شد \n حال لیست کراش های خود را وارد کنید");
                await _telegramBotClient.SendTextMessageAsync(user.ChatId,
                   $"ای دی اینستاگرام انها را در هر خط وارد کنید \n مثال : \n @HHHHH \n @SSSSS", ParseMode.Default, false, false, 0,
                   _utilitiesWrapper.KeyboardFactory.GetKeyboard(KeyboardType.Back));
            }
            else
            {
                var userUrl = _utilitiesWrapper.Utility.GetUserLink(user.UserUid);
                await _telegramBotClient.SendTextMessageAsync(user.ChatId,
                   $"لطفا پیج زیر را دنبال کرده و دوباره دکمه اعتبار سنجی را بفشارید \n @mr.mohande3", ParseMode.Default, false, false, 0,
                   new InlineKeyboardMarkup(new InlineKeyboardButton
                   {
                       Text = "ورود به پیج",
                       Url = $"https://www.instagram.com/mr.mohande3"
                   }));

            }
        }
        private async void SignUpStatus(UpdateBot updateBot)
        {
            user.UserStatus = UserStatus.SignUp;
            await _repositoryWrapper.Users.UpdateEntryAsync(user);
            await _telegramBotClient.SendTextMessageAsync(updateBot.message.Chat.id,
               "به ثبت نام خوش امدید لطفا ای دی اینستاگرام خود را واردی کنید");
            await _telegramBotClient.SendTextMessageAsync(updateBot.message.Chat.id,
               "مثال : @HHHHH", ParseMode.Default, false, false, 0,
               _utilitiesWrapper.KeyboardFactory.GetKeyboard(KeyboardType.Back));
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
                await _repositoryWrapper.Users.UpdateEntryAsync(user);
                await _telegramBotClient.SendTextMessageAsync(chatid,
                   "به ربات کراشیسم خوش امدید \n لطفا برای دیدن تمامی امکانات و اعتبار سنجی لطفا ای دی اینستاگرام خود را واردی کنید");
                await _telegramBotClient.SendTextMessageAsync(chatid,
                   "مثال : @HHHHH", ParseMode.Default, false, false, 0,
                   _utilitiesWrapper.KeyboardFactory.GetKeyboard(KeyboardType.Back));
            }

        }
    }
}
