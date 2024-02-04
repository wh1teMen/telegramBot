using Telegram.Bot;
using Telegram.Bot.Types;

namespace TagHelpers_andApiTelegram.Controllers.Commands
{
    public class RegisterCommand : ICommand, IListener
    {

        public static ChatId idChat;
        public static Update refUpdate;
        public  TelegramBotClient Client => Bot.GetTelegramBot();
        public string Name => "/registration";

        public CommandExecutor Exceutor { get; }
        public RegisterCommand(CommandExecutor exceutor)
        {
            Exceutor = exceutor;
        }
        public RegisterCommand()
        {
        }

        private string? phone;
        private string? name;



        public async Task ExecuteAsync(Update update)
        {
            long chatId = update.Message.Chat.Id;
            Exceutor.StartListen(this);
            await Client.SendTextMessageAsync(chatId, $"Введите номер!");
            return;
           
        }

            public  async Task GetUpdate(Update update)
            {
                long chatId = update.Message.Chat.Id;
                if (update.Message.Text == null) return;

                if (phone == null)
                {
                    phone = update.Message.Text;
                    await Client.SendTextMessageAsync(chatId, "Введите имя!");return;
                }
                else
                {
                    name = update.Message.Text;
                    await Client.SendTextMessageAsync(chatId, "Регистрация завершена!");return;
                    Exceutor.StopListen();
                }

            }
        }
    
}

