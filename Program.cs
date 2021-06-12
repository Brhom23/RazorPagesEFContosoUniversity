using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RazorPagesEFContosoUniversity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesEFContosoUniversity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            //åĞå ÇáÃßæÏ æÇáÏÇáÉ ÇáÊí ÊáíåÇ ÍÊì íŞæã ÇáÈÑäÇãÌ
            //ÈÚãá ŞÇÚÏÉ ÇáÈíÇäÇÊ Åä áã Êßä ãæÌæÏÉ Ãæ ÇáÊÚÏíá Úáì ÇáÌÏÇæá
            //Åä Êã ÅÖÇİÉ ÍŞá ÌÏíÏ İí İÆÉ ÃÍÏ ÇáÌÏÇæá
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();
        }
        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MyContext>();
                    //ÇáÓØÑ ÇáÊÇáí áßí íäÔÁ ŞÇÚÏÉ ÇáÈíÇäÇÊ
                    //æíÊã ÇáÚãá ãÚå ÇËäÇÁ ÊÌÑÈÉ ÇáÈÑäÇãÌ
                    //æáÇ íäÔÆ ÌÏæá ãÍİæÙÇÊ ÇáÊÑÍíáÇÊ
                    //æÚäÏ ÇáÚãá ÇáİÚáí ãÚ
                    //ŞÇÚÏÉ ÇáÈíÇäÇÊ íÌÈ ÇíŞÇİå
                    //context.Database.EnsureCreated();
                     SeedData.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
