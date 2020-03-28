using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramBotCore.Models.Context;
using TelegramBotCore.Repositories;
using TelegramBotCore.Services;
using TelegramBotCore.Utilities;

namespace TelegramBotCore
{
    public class Startup
    {
        public static string CrushBot { get; } = "509847876:AAG0aTib65R2nDFZmzu7zCbRydHBJ8MpuwI";
        public static string UnknownBot { get; } = "406964183:AAHUJvqN5EdFXvNv8zRjqpLiVzvbpoX3-dY";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services
                .AddDbContext<BotContext>((options) =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("LocalConnection"));
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    //options.UseSqlServer(Configuration.GetConnectionString("ServerConnection"));
                }, ServiceLifetime.Scoped);
            services.AddScoped<BotService>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<UnknownBot>();
            services.AddScoped<StatusService>();
            services.AddScoped<IUtilitiesWrapper, UtilitiesWrapper>();
            services.AddScoped<UnStatusService>();
            services.AddScoped<InlineService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
