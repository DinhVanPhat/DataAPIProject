using dotenv.net;
using Microsoft.Extensions.DependencyInjection;
using DataAPIProject;

public class Program
{
    public static void Main(string[] args)
    {
        // Tải biến môi trường từ file .env
        DotEnv.Load(".env");

        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                if (env.IsDevelopment())
                {
                    config.AddEnvironmentVariables();
                }
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

