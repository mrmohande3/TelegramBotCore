namespace TelegramBotCore.Models.Bot
{
    public class CallBackQueryBot
    {
        public UserBot from { get; set; }
        public MessageBot message { get; set; }
        public string data { get; set; }
        public string chat_instance { get; set; }
        public string inline_message_id { get; set; }

    }
}