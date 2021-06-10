﻿using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using AAG.Global.Common;
using ETL.DataLoader.Generic.Services;
using NLog.Web;

namespace ETL.DataLoader.Generic
{
    public static class ConsoleAppHost
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.UseStartup<ConsoleAppStartup>();
              })
              .ConfigureLogging(logging =>
              {
                  logging.ClearProviders();
                  logging.SetMinimumLevel(LogLevel.Information);
              })
              .ConfigureServices(services =>
              {
                  services.AddSingleton<ApplicationArguments>(new ApplicationArguments(args));
                  services.AddHostedService<LoaderService>();
              })
             .ConfigureAppConfiguration(builder =>
             {
                 builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
             })
              .UseNLog();
    }
}