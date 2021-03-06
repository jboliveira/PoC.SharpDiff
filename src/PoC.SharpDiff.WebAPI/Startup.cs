﻿using System;
using CorrelationId;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PoC.SharpDiff.Domain.Repositories;
using PoC.SharpDiff.Domain.Services;
using PoC.SharpDiff.Persistence.Repositories;
using PoC.SharpDiff.WebAPI.Infrastructure.Extensions;
using PoC.SharpDiff.WebAPI.Infrastructure.Swagger;
using PoC.SharpDiff.WebAPI.Services;
using Serilog;

// Applies web API-specific behaviors to all controllers in the assembly.
[assembly: ApiController]

namespace PoC.SharpDiff.WebAPI
{
    /// <summary> API Startup class </summary>
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IHostEnvironment hostEnvironment;

        public Startup(IConfiguration config, IHostEnvironment env)
        {
            configuration = config ?? throw new ArgumentNullException(nameof(config));
            hostEnvironment = env ?? throw new ArgumentNullException(nameof(env));
        }

        /// <summary> This method gets called by the runtime in order to add services to the container. </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddHealthChecks();

            var connString = configuration.GetConnectionString("SharpDiffDatabase");
            services.AddCustomDbContext(connString, hostEnvironment.IsEnvironment("Testing"));
            services.AddCustomControllers();
            services.AddCustomApiVersioning();
            services.AddCorrelationId();
            services.AddCustomSwagger();
            services.AddCors();

            RegisterServices(services);
        }

        /// <summary> This method gets called by the runtime in order to configure the HTTP request pipeline. </summary>
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSerilogRequestLogging();
                app.UseDeveloperExceptionPage();
                app.UseCustomCors();
            }
            else
            {
                app.UseForwardedHeaders();
                // See https://aka.ms/aspnetcore-hsts
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseApiVersioning();

            app.UseStaticFiles();
            app.UseCustomSwagger();

            app.UseCorrelationId(new CorrelationIdOptions { UseGuidForCorrelationId = true });
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }

        /// <summary>
        /// .NET Native DI
        /// </summary>
        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IContentRepository, ContentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IContentService, ContentService>();
        }
    }
}
