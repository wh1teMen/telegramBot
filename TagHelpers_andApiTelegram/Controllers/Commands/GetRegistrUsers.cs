using TagHelpers_andApiTelegram.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TagHelpers_andApiTelegram.Controllers.Commands
{
    public class GetRegistrUsers : ICommand, IListener
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string Name => "/GetRegistrUsers";
        public CommandExecutor Exceutor { get; }        
        public GetRegistrUsers(CommandExecutor exceutor)
        {
            Exceutor = exceutor;
        }

        public GetRegistrUsers()
        {
        }

        public async Task ExecuteAsync(Update update)
        {
            long chatId = update.Message.Chat.Id;
            try
            {
                
                var listUsers = BotController.db.RegistrBDs.ToList();
                string users = string.Empty;
                listUsers.ForEach(x => users += $"Имя:{x.Name} {x.NumberPhone}\n");
                await Client.SendTextMessageAsync(chatId, $"Зарегестрированные пользователи:\n{users}");
            }
            catch{}

        }

    }
}
