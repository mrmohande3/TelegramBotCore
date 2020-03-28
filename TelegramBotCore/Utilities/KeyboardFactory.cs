using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotCore.Utilities
{
    public enum KeyboardType
    {
        MainMenu,
        Start,
        Back,
        CrushList,
        GiveUserName
    }
    public class KeyboardFactory
    {
        public ReplyKeyboardMarkup GetKeyboard(KeyboardType keyboardType)
        {
            switch (keyboardType)
            {
                case KeyboardType.MainMenu:
                    return new ReplyKeyboardMarkup(new List<IEnumerable<KeyboardButton>>
                    {
                        new []{new KeyboardButton("چت های ناشناس من"), },
                        new []{new KeyboardButton("لیست کراش های من"), },
                        new []{new KeyboardButton("تصویر اختصاصی من"), }
                    });
                    break;
                case KeyboardType.Start:
                    return new ReplyKeyboardMarkup(new KeyboardButton("ثبت نام"));
                    break;
                case KeyboardType.Back:

                    return new ReplyKeyboardMarkup(new KeyboardButton("بازگشت"));
                    break;
                case KeyboardType.GiveUserName:
                    return new ReplyKeyboardMarkup(new KeyboardButton("اعتبار سنجی"));
                    break;
                default:
                    return null;
                    break;
            }
        }

    }
    
}
