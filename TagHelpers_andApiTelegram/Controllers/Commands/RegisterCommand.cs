using TagHelpers_andApiTelegram.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TagHelpers_andApiTelegram.Controllers.Commands
{
    public class RegisterCommand : ICommand, IListener
    {

        
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string Name => "/registration";
        public CommandExecutor Exceutor { get; }
        private RegistrBD reg=new RegistrBD();
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
            await Client.SendTextMessageAsync(chatId, $"Введите номер телефона!");           

        }

        public async Task GetUpdate(Update update)
        {
            long chatId = update.Message.Chat.Id;
            if (update.Message.Text == null) return;

            if (phone == null)
            {
               
                phone = update.Message.Text;
                reg.NumberPhone = phone;
                await Client.SendTextMessageAsync(chatId, "Введите имя!");
            }
            else
            {
               
                name = update.Message.Text;   
                reg.Name = name;
                BotController.db.RegistrBDs.Add(reg);
                await BotController.db.SaveChangesAsync();
                await Client.SendTextMessageAsync(chatId, "Регистрация завершена!");return;               
                Exceutor.StopListen();
            }
            

        }
    }

}