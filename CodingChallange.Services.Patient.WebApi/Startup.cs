using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingChallange.Repositories.Patient.EFCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using CodingChallange.Services.Patient;
using CodingChallange.Repositories.Patient;
using Microsoft.AspNetCore.Mvc.Versioning;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using AutoMapper;
using CodingChallange.Services.Patient.WebApi.Mappers;
using Sieve.Services;
using Sieve.Models;

namespace CodingChanllage.Patient.Service.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                    opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                });
            services.AddDbContext<PatientDbContext>(options => 
                options.UseInMemoryDatabase(databaseName: Configuration["DatabaseSetting:DatabaseName"])
                );
            services.AddLogging();
            services.AddApiVersioning(o => {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.Configure<SieveOptions>(Configuration.GetSection("Sieve"));
            services.AddAutoMapper(typeof(PatientModelAndViewModelMappingProfile));
            services.AddScoped<ISieveProcessor, PatientPaginProcessor>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IPatientManager, PatientManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
