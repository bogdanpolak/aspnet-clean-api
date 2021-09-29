using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CleanApi.WebApi
{
    public class AspNetHostBuilder
    {
        public static void BuildAndRun(string[] args)
        {   
            CreateHostBuilder(args).Build().Run();
        }
        
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}