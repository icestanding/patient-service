using CodingChanllage.Patient.Service.WebApi;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;

namespace CodingChallange.Patient.Service.ConsoleApp.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: false)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();
        }
    }
}
