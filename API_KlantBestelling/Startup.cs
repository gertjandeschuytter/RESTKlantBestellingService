using BusinessLayer_KlantBestelling.Interfaces;
using BusinessLayer_KlantBestelling.Services;
using DataLayer_KlantBestelling;
using DataLayer_KlantBestelling.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_KlantBestelling {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KlantBestelling;Integrated Security=True";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddControllers();
            services.AddSingleton<IBestellingRepository>(r => new BestellingRepositoryADO(connectionString));
            services.AddSingleton<IKlantRepository>(r => new KlantRepositoryADO(connectionString));
            services.AddSingleton<BestellingService>();
            services.AddSingleton<KlantService>();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_KlantBestelling", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_KlantBestelling v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
