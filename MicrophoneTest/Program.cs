
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace MicrophoneTest
{


    public class Program
    {


        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
        

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(System.IO.Directory.GetCurrentDirectory())
                .UseKestrel(c => c.AddServerHeader = false)
                .UseIISIntegration()
                .UseStartup<Startup>();
        }


    }


}
