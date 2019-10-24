using Microsoft.AspNetCore.Builder;

namespace PoC.SharpDiff.WebAPI.Infrastructure.Extensions
{
    /// <summary> Application Builder Extensions  </summary>
    public static class ApplicationBuilderExtensions
    {
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
