
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TagHelpers_andApiTelegram.Controllers.Commands
{
    public class ClearHistory : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string Name => "/clearHistory";       
        public async Task ExecuteAsync(Update update)
        {
            long chatId = update.Message.Chat.Id;
            try
            {
                BotController.db.messagesDB.ExecuteDelete();
                await BotController.db.SaveChangesAsync();
                await Client.SendTextMessageAsync(chatId, $"История отчищена");
            }
            catch { }

        }
    }
}
