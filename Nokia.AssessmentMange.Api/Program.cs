using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Autofac.Extensions.DependencyInjection;
namespace Nokia.AssessmentMange.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                
                .Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
             //.ConfigureLogging((hostingContext, logging) => {
             //    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
             //})
             .ConfigureServices(services => services.AddAutofac())

            .UseStartup<Startup>()
            .UseNLog()
            ;
    }
}
