using Bloger.DataAccess.Concrete;
using Bloger.Entity.Concrete;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

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

            // proje seviyesinde authorize iþlemi
            builder.Services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });


            // identity ile cookie çýkýþ iþlemi
            builder.Services.ConfigureApplicationCookie(opts =>
            {
                //Cookie settings
                opts.Cookie.HttpOnly = true;
                opts.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                opts.AccessDeniedPath = new PathString("/Login/AccessDenied"); // eriþimin reddedildiði durumda gitmesi gerek yer
                opts.AccessDeniedPath = new PathString("/Login/AccessDenied/");
                opts.LoginPath = "/Blog/Index/";
                opts.SlidingExpiration = true;
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}"); // error sayfasý

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication(); // üyeyi kontol eder
            app.UseAuthorization(); // yetkiyi konrol eder  

            app.UseSession();


            app.MapControllerRoute( // area için program cs eklentisi
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Blog}/{action=Index}/{id?}");

            app.Run();
        }
    }
}