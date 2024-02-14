using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using TagHelpers_andApiTelegram.Controllers.Commands;
using TagHelpers_andApiTelegram.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TagHelpers_andApiTelegram.Controllers
{
    [ApiController]
    [Route("/")]
    public class BotController : Controller
    {
        private readonly ILogger<HomeController> _logger;   
        private static UpdateDistributor<CommandExecutor> _distributor = new UpdateDistributor<CommandExecutor>();
        public RegisterCommand reg = new RegisterCommand();
        MessageModel meS=new MessageModel();
        public static List<MessageModel> chatM=new List<MessageModel>();
        public static AppDb db;

        public static ChatId idChat;
        
        public BotController(ILogger<HomeController> logger,AppDb context)
        {

            _logger = logger;
            db = context;
        }
        [HttpPost]
        public async Task Post(Update update)
        {
          
            if (update.Message == null) return;
            idChat = update.Message.Chat.Id;         
            chatM.Add(new MessageModel { Text = update.Message.Text});
            Get();
            await _distributor.GetUpdate(update);                     
            await Console.Out.WriteLineAsync(update.Message.Text);
            if(update.Message.Photo!=null || update.Message.Video!=null|| update.Message.Audio != null) { return; }            
                db.messagesDB.Add(new MessageModel { Text = update.Message.Text });
                await db.SaveChangesAsync();
            
        }

        [HttpGet]
        public IActionResult Get()
        {           
            
            return View(chatM); 
        }



       



         [HttpPost("Bot/But")]
       
        public ActionResult But([FromForm]string mes)
        {          
            TelegramBotClient bot = Bot.GetTelegramBot();
            chatM.Add(new MessageModel { Text = mes });            
            Console.Out.WriteLine(mes);
            var botMes = bot.SendTextMessageAsync(idChat, mes).Result;
            Get();
            return RedirectToAction("Get"); 
        }

    }
}
