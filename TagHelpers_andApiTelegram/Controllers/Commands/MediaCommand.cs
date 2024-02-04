using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;


namespace TagHelpers_andApiTelegram.Controllers.Commands
{
    public class MediaCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();

        public string Name { get; set; }         
        
        public async Task ExecuteAsync(Update update)
        {
            long chatId = update.Message.Chat.Id;
            var messege = update.Message;
            if(messege.Photo!=null)
            {
                try
                {
                    var fileId = update.Message.Photo.Last().FileId;
                    var fileInfo = await Client.GetFileAsync(fileId);
                    var filePath = fileInfo.FilePath;
                    string savePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\file.jpg";
                    await using FileStream fileStream = System.IO.File.OpenWrite(savePath);
                    await Client.DownloadFileAsync(filePath, fileStream);
                    fileStream.Close();
                    await using Stream stream = System.IO.File.OpenRead(savePath);
                    Message message = await Client.SendPhotoAsync(chatId, InputFile.FromStream(stream: stream, fileName: "fileBack.jpg"));
                    return;
                }
                catch { }

            }
            if (messege.Document != null)
            {
                try
                {
                    var fileId = update.Message.Document.FileId;
                    var fileInfo = await Client.GetFileAsync(fileId);
                    var filePath = fileInfo.FilePath;
                    string savePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\{messege.Document.FileName}";
                    await using FileStream fileStream = System.IO.File.OpenWrite(savePath);
                    await Client.DownloadFileAsync(filePath, fileStream);
                    fileStream.Close();
                    await using Stream stream = System.IO.File.OpenRead(savePath);
                    Message message = await Client.SendDocumentAsync(chatId, InputFile.FromStream(stream, messege.Document.FileName));
                    return;
                }
                catch { }
            }

            if (messege.Audio != null)
            {
                try
                {
                    var fileId = update.Message.Audio.FileId;
                    var fileInfo = await Client.GetFileAsync(fileId);
                    var filePath = fileInfo.FilePath;
                    string savePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\{messege.Audio.FileName}";
                    await using FileStream fileStream = System.IO.File.OpenWrite(savePath);
                    await Client.DownloadFileAsync(filePath, fileStream);
                    fileStream.Close();
                    await using Stream stream = System.IO.File.OpenRead(savePath);
                    Message message = await Client.SendAudioAsync(chatId, InputFile.FromStream(stream, "Фууу...Забери обратно" + messege.Audio.FileName));
                    return;

                }
                catch { }
               
            }
            if (messege.Video != null)
            {
                try
                {
                    var fileId = update.Message.Video.FileId;
                    var fileInfo = await Client.GetFileAsync(fileId);
                    var filePath = fileInfo.FilePath;
                    string savePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\{messege.Video.FileName}";
                    await using FileStream fileStream = System.IO.File.OpenWrite(savePath);
                    await Client.DownloadFileAsync(filePath, fileStream);
                    fileStream.Close();
                    await using Stream stream = System.IO.File.OpenRead(savePath);
                    Message message = await Client.SendAudioAsync(chatId, InputFile.FromStream(stream, "Фууу...Забери обратно" + messege.Video.FileName));
                    return;

                }
                catch { }

            }
        }
    }
    }

