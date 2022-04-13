using System;
using Microsoft.AspNetCore;

namespace Task_4
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
  
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
  WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();

    }
}