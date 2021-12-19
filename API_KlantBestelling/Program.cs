using BusinessLayer_KlantBestelling.Interfaces;
using DataLayer_KlantBestelling;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_KlantBestelling {
    public class Program {
        public static void Main(string[] args) {
        //string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KlantBestelling;Integrated Security=True";
        //    KlantRepositoryADO kRepo = new KlantRepositoryADO(connectionString);
        //    kRepo.GeefKlant(1);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
