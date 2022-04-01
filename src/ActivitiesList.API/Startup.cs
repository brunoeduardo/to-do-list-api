using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ActivitiesList.Data.Context;
using ActivitiesList.Data.Repositories;
using ActivitiesList.Domain.Interfaces.Repositories;
using ActivitiesList.Domain.Interfaces.Services;
using ActivitiesList.Domain.Services;
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

namespace ActivitiesList.API
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
            services
                .AddDbContext<DataContext>(options =>
                    options
                        .UseSqlite(Configuration
                            .GetConnectionString("Default")));

            services.AddScoped<IActivityRepo, ActivityRepo>();
            services.AddScoped<IGeneralRepo, GeneralRepo>();
            services.AddScoped<IActivityService, ActivityService>();
            
            services
                .AddControllers()
                .AddJsonOptions(options =>
                    options
                        .JsonSerializerOptions
                        .Converters
                        .Add(new JsonStringEnumConverter()));
            services
                .AddSwaggerGen(c =>
                {
                    c
                        .SwaggerDoc("v1",
                        new OpenApiInfo {
                            Title = "ActivitiesList.API",
                            Version = "v1"
                        });
                });

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app
                    .UseSwaggerUI(c =>
                        c
                            .SwaggerEndpoint("/swagger/v1/swagger.json",
                            "ActivitiesList.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app
                .UseCors(option =>
                    option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
