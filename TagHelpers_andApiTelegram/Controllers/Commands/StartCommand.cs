using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TagHelpers_andApiTelegram.Controllers.Commands
{
    public class StartCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();


        public string Name => "/start";    

        public async Task ExecuteAsync(Update update)
        {
            try
            {
                long chatId = update.Message.Chat.Id;
                string? userName = update.Message.From.Username;
                string com = string.Empty;
                CommandExecutor.commands.ForEach(x => com += x.Name + "\n");
                await Client.SendStickerAsync(
                chatId: chatId,

                 sticker: InputFile.FromUri("https://github.com/TelegramBots/book/raw/master/src/docs/sticker-dali.webp"));
                await Client.SendTextMessageAsync(chatId, com);
                return;
            }
            catch { }


        }
    }
}