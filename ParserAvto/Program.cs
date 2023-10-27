using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ParserAvto.Core;
using ParserAvto.Core.AvtoParser;
using ParserAvto.Core.PersonBuild;
using ParserAvto.DAL;
using ParserAvto.Handlers;

namespace ParserAvto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => options.LoginPath = "/login");
            builder.Services.AddAuthorization();


            builder.Services.AddScoped<IParser, AvtoParser>();
            builder.Services.AddSingleton<IParserSettings, AvtoSettings>();

            builder.Services.AddScoped<PageLoader>();

            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddScoped<PersonBuilder>();
            builder.Services.AddScoped<ListAvto>();
            builder.Services.AddScoped<BotHandler>();
            builder.Services.AddScoped<SaveAvtoForDb>();

            builder.Services.AddHostedService<BackgroundParserService>();

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

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");



            app.Run();
        }
    }
}