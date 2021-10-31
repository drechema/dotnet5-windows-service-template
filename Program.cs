using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try {
                CreateHostBuilder(args).Build().Run();
                return;
            } catch (Exception ex) {
                Log.Fatal(ex,"Fatal problem starting service");
                return;
            } finally {
                Log.CloseAndFlush();
            } 
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) {
            return Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
                    Log.Information("-------- Service Configuration ----------");
                    Log.Information("Environment mode: {mode}",hostContext.HostingEnvironment.EnvironmentName);
                    WorkerSettings settings = configuration.GetSection("Worker").Get<WorkerSettings>();
                    Log.Information("Worker setting1 = {value}",settings.Setting1);
                    services.AddSingleton(settings);                    
                    services.AddHostedService<Worker>();
                }).UseSerilog();            
        }
    }
}
