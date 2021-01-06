
using System;

namespace HomeMQ.MainContext
{
    class Program
    {
        static void Main(string[] args)
        {
            
            using var context = new HomeMQMainContext();
            var location = new LocationSQL(name: "Patio", room: "Outside");
            context.Add(location);
            context.SaveChanges();
            ////CreateHostBuilder(args).Build().Run();
            //var host = CreateHostBuilder(args).Build();

            //using var scope = host.Services.CreateScope();
            //var services = scope.ServiceProvider;

            ////try
            ////{
            ////    SeedData.Initialize(services);
            ////}
            ////catch (Exception ex)
            ////{
            ////    var logger = services.GetRequiredService<ILogger<Program>>();
            ////    logger.LogError(ex, "An error occurred seeding the DB.");
            ////}

            //host.Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
        //    {
        //        webBuilder.UseStartup<Startup>();
        //    });

    }
}
