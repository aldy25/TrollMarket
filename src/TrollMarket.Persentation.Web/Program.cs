using Microsoft.AspNetCore.Authentication.Cookies;
using TrollMarket.DataAcces;
using TrollMarket.Persentation.Web.Configuration;
using TrollMarket.Persentation.Web.Services;

namespace Winterhold.Persentation.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.AddConsole(); //->intansiasi dari Ilogger


            //dependensi injection
            IServiceCollection services = builder.Services;
            Dependencies.AddDataAccessServices(services, builder.Configuration);
            services.AddBusinessService();
            services.AddScoped<AuthService>();
            services.AddScoped<AccountService>();
            services.AddScoped<MerchandiseService>();
            services.AddScoped<ShopService>();
            services.AddScoped<ShipperService>();
            services.AddScoped<CartService>();
            services.AddScoped<HistoryService>();
            services.AddScoped<HomeService>();
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.LoginPath = "/Auth/Login";
                   options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                   options.AccessDeniedPath = "/Auth/AccesDanied";
               });
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