﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ESFA.DC.LARS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(true)
                .UseStartup<Startup>();
    }
}
