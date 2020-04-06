using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;
using TelegramBotCore.Services;

namespace TelegramBotCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /* var config = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();
             Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
             try
             {
                 Log.Information("Application Start Up");
                 CreateHostBuilder(args).Build().Run();
             }
             catch (Exception e)
             {
                 Log.Fatal(e, "The Application not start");
             }
             finally
             {
                 Log.CloseAndFlush();
             }*/
            var host = CreateHostBuilder(args).Build();
            using (var scop = host.Services.CreateScope())
            {
                var init = scop.ServiceProvider.GetService<DBInitializer>();
                init.Seed();
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
