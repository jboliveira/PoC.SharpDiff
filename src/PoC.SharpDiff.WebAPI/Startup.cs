﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Applies web API-specific behaviors to all controllers in the assembly.
[assembly: ApiController]

namespace PoC.SharpDiff.WebAPI
{
	/// <summary> API Startup class </summary>
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public IHostingEnvironment CurrentEnvironment { get; }

		public Startup(IConfiguration configuration, IHostingEnvironment currentEnvironment)
		{
			Configuration = configuration;
			CurrentEnvironment = currentEnvironment;
		}

		/// <summary> This method gets called by the runtime in order to add services to the container. </summary>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

			RegisterServices(services);
		}

		/// <summary> This method gets called by the runtime in order to configure the HTTP request pipeline. </summary>
		public void Configure(IApplicationBuilder app)
		{
			if (CurrentEnvironment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// See https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
			app.UseWelcomePage();
		}

		/// <summary>
		/// .NET Native DI
		/// </summary>
		private static void RegisterServices(IServiceCollection services)
		{
			// Register here...
		}
	}
}
