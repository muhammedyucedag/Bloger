using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloger.Entity.Concrete;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloger.DataAccess.Concrete
{
    public class Context : IdentityDbContext<User, Role, int>
    {
        // proje ayağa kalktığında database varsa onu kullan yoksa oluştur.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // Entityde bulunan classlarımı setleme işlemi yaptım
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Image> Image { get; set; }


        // veritabanımızı configure ediyoruz.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(
                    //"Server=DESKTOP-M8JLN9L\\SQLEXPRESS;Database=Bloger; integrated security=true;");
                    //"Data Source=104.247.162.242\\MSSQLSERVER2017; Initial Catalog=akadem58_my; Persist Security Info=True; User ID=akadem58_my; Password=Rnqo30*59 providerName = System.Data.SqlClient");
                    "Server = 104.247.162.242\\MSSQLSERVER2017; Database = akadem58_my; User Id = akadem58_my; password = Rnqo30*59; Trusted_Connection = False; MultipleActiveResultSets = true");
            }

        }
    }
}
