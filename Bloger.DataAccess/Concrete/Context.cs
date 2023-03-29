using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloger.Entity.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloger.DataAccess.Concrete
{
    public class Context:IdentityDbContext<User,Role,int>
    {
        // proje ayağa kalktığında database varsa onu kullan yoksa oluştur.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // Entityde bulunan classlarımı setleme işlemi yaptım
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }


        // veritabanımızı configure ediyoruz.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(
                    "Server=DESKTOP-M8JLN9L\\SQLEXPRESS;Database=Bloger; integrated security=true;");
            }

        }
    }
}
