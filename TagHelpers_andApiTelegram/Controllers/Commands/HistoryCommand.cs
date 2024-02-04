using Telegram.Bot;
using Telegram.Bot.Types;

namespace TagHelpers_andApiTelegram.Controllers.Commands
{
    public class HistoryCommand : ICommand
    {

        public TelegramBotClient Client => Bot.GetTelegramBot();


        public string Name => "/history";

        public async Task ExecuteAsync(Update update)
        {
            try
            {
                long chatId = update.Message.Chat.Id;
                string historyResult = string.Empty;
                var listHistory = BotController.db.messagesDB.ToList();
                listHistory.ForEach(el => historyResult += el.Text + "\n");
                await Client.SendTextMessageAsync(chatId, historyResult);
                return;
            }
            catch { }
        }
    }
}
