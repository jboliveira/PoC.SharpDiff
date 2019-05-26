using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PoC.SharpDiff.WebAPI.Infrastructure.Swagger
{
	/// <summary> Swagger Extensions  </summary>
	public static class SwaggerExtensions
    {
        /// <summary> Adds Swagger service and configure. </summary>
        /// <param name="services">IServiceCollection dependency</param>
        public static void AddCustomSwagger(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents.
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
        }

        /// <summary> Adds a Swagger middleware into the pipeline. </summary>
        /// <param name="app">IApplicationBuilder dependency</param>
        public static void UseCustomSwagger(this IApplicationBuilder app)
        {
            var provider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });
        }
    }
}
