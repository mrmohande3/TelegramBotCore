using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TelegramBotCore.Models
{
    public enum UserStatus
    {
        NULL,
        [Description("/start")]
        Start,
        [Description("ثبت نام")]
        SignUp,
        [Description("اعتبار سنجی")]
        GiveUserName,
        [Description("افزودن کراش")]
        GiveCrashes,
        [Description("من کراش چند نفرم ؟")]
        HowManyCrushIAm,
        [Description("چت های ناشناس من")]
        ChatUnknown,
        [Description("لیست کراش های من")]
        CrushList,
        [Description("پروفایل من")]
        Profile,
        [Description("تصویر اختصاصی من")]
        UnicornPic,
        [Description("بستن و قطع چت")]
        CloseChat,
        [Description("/back")]
        Back,
    }
}
