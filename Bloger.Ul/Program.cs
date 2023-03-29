using Bloger.DataAccess.Concrete;
using Bloger.Entity.Concrete;

namespace Bloger.Ul
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //identity
            builder.Services.AddDbContext<Context>();
            builder.Services.AddIdentity<User, Role>(
                x =>
                {
                    x.Password.RequireUppercase = false;
                    x.Password.RequireNonAlphanumeric = false;

                }).AddEntityFrameworkStores<Context>();
            builder.Services.AddHttpContextAccessor();


            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddSession();

			var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Blog}/{action=Index}/{id?}");

            app.Run();
        }
    }
}