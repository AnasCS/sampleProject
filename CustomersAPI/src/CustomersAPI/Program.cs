﻿using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Resources;
using System.Reflection;

namespace CustomerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://0.0.0.0:6000")
                // Services can be registered with the ASP.NET Core DI container at IWebHost build-time
                // by calling ConfigureServices on the WebHostBuilder.
                //
                // This is an easy way of injecting services that wouldn't otherwise be available in
                // the web application's Startup class (for example, web apps running in a Service Fabric
                // application could have their service context injected here).
                .ConfigureServices(serviceCollection =>
                {
                    serviceCollection.AddSingleton(new ResourceManager("CustomersAPI.Resources.StringResources",
                                                      typeof(Startup).GetTypeInfo().Assembly));
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
