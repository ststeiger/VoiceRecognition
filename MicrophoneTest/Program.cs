using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
            // ListAvailableRecognizers();
            CreateWebHostBuilder(args).Build().Run();
        }


        public static void ListAvailableRecognizers()
        {
            // Speech SDK needs to be installed 
            // https://www.microsoft.com/en-us/download/details.aspx?id=10121

            foreach (System.Speech.Recognition.RecognizerInfo ri in System.Speech.Recognition.SpeechRecognitionEngine.InstalledRecognizers())
            {
                System.Console.WriteLine(ri.Culture.TwoLetterISOLanguageName);
            }
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }


    }


}
