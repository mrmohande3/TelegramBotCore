using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramBotCore.Models
{
    public class BotSingleTon
    {
		private static ITelegramBotClient telegramBotClient = null;
		public static ITelegramBotClient BotClient
		{
            get
            {
                if(telegramBotClient==null)
                    telegramBotClient = new TelegramBotClient("509847876:AAG0aTib65R2nDFZmzu7zCbRydHBJ8MpuwI");
                return telegramBotClient;
            }
		}

	}
}
