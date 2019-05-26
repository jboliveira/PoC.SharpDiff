using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace PoC.SharpDiff.WebAPI.Infrastructure.Extensions
{
	/// <summary> Application Builder Extensions  </summary>
	public static class ApplicationBuilderExtensions
    {
        /// <summary> Adds a HealthCheck middleware into the pipeline. </summary>
        /// <param name="app">IApplicationBuilder dependency</param>
        public static void UseCustomHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });
        }

        /// <summary> Adds a CORS middleware into the pipeline. </summary>
        /// <param name="app">IApplicationBuilder dependency</param>
        public static void UseCustomCors(this IApplicationBuilder app)
        {
            app.UseCors(options =>
            {
                options
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        }
    }
}
