using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace ETL.DataLoader.Generic
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Set current directory path and error file name.
            var currentDirectoryPath = Directory.GetCurrentDirectory();
            var startupErrorLogFile = $"_startup-errors-{DateTime.Now:yyyy-MM-dd}.log";

            try
            {
                // Run the application.
                await ConsoleAppHost
                    .CreateHostBuilder(args)
                    .RunConsoleAsync();
            }
            catch (Exception e)
            {
                var startupErrorLogFilePath = Path.Combine(currentDirectoryPath, startupErrorLogFile);
                using StreamWriter streamWriter = new StreamWriter(startupErrorLogFilePath, append: true);
                streamWriter.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss:fffffff}] ERROR | {e.Message}");
                streamWriter.WriteLine($"Object: {JsonConvert.SerializeObject(e)}{Environment.NewLine}");
            }
        }
    }
}