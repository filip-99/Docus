using CandidateApp.Data;
using CandidateApp.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateApp
{
    public class Startup
    {
        // Deklarišemo promenjivu u koju smeštamo putanju ka bazi
        public string ConnectionString { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Dodeljujemo ovoj promenjivost vrednost, odmah pri pozivu ove klase u nekoj drugoj klasi
            // Ovaj naziv "DefaultConnectionString" treba da bude isti kao naziv baze u fajlu appsettings.json
            ConnectionString = Configuration.GetConnectionString("DefaultConnectionString");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            // Nastavljamo dalje sa konfiguracijom baze, sada je potrebno da odmah ispod services.AddControllers(); izvršimo konfiguraciju konteksta sa SQL bazom podataka
            // Potrebno je da instaliramo i Microsoft.EntityFrameworkCore.SqlServer verziju 5.0.17 zbog .Net Core SDK verzije 5
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionString));

            // Potrebno je da konfigurišemo servis klasu BooksService.cs
            // Pa će mo koristiti servis, koji omogućava prelaz parametra u bazu
            services.AddTransient<CandidatesService>();
            services.AddTransient<FormsService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CandidateApp", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CandidateApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Sada u Configure metodi inicializujemo naša bazu podataka tj.. ubacujemo u nju podatke iz klase "AppDbInitializer"
            // Tačnije u toj klasi se nalazi metoda Seed() sa prosleđenom promenjivom tipa IApplicationBuilder
            // Dakle kada startujemo program okinuće se ubacivanje u bazu podataka
            // AppDbInitializer.Seed(app);
        }
    }
}
