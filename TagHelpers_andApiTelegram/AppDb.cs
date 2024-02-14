using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using TagHelpers_andApiTelegram.Models;

namespace TagHelpers_andApiTelegram
{
    public class AppDb : DbContext
    {

        public DbSet<MessageModel> messagesDB { get; set; } = null;
        public DbSet<RegistrBD> RegistrBDs { get; set; } = null;
        public AppDb(DbContextOptions<AppDb> options) : base(options)
        {
           
           //Database.EnsureDeleted();
            Database.EnsureCreated(); ///Проверяет сущствует база данных, если она не существует - создает базуданных.


        }

    }
}
