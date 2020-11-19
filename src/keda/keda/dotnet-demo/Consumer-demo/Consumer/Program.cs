using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        
        private static IHostEnvironment HostEnvironment { get; set; }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
                    var applicationVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    
                    HostEnvironment = context.HostingEnvironment;

                    config.SetBasePath(HostEnvironment.ContentRootPath);
                    config.AddJsonFile("appsettings.json", optional: false)
                        .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true); //TODO: optional false when we have the other files
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}