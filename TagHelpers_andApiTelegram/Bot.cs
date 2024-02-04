using Telegram.Bot;

public class Bot
{
    private static TelegramBotClient client { get; set; }

    public static TelegramBotClient GetTelegramBot()
    {
        if (client != null)
        {
            return client;
        }
        client = new TelegramBotClient("6973188842:AAFjvMHG8ffyzmgdJy8EF_5BYsofWVvntNI");
        return client;
    }
}
