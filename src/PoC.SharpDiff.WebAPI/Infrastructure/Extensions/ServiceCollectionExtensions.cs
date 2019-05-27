using System.Net.Mime;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PoC.SharpDiff.WebAPI.Persistence.Contexts;

namespace PoC.SharpDiff.WebAPI.Infrastructure.Extensions
{
    /// <summary> Service Collection Extensions  </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary> Register the given context and configures to connect to a SqlServer database. </summary>
        /// <param name="services">IServiceCollection dependency</param>
        /// <param name="connString">ConnectionString.</param>
        /// <param name="isTestingEnvironment">Is Testing Environment.</param>
        public static void AddCustomDbContext(this IServiceCollection services, string connString, bool isTestingEnvironment)
        {
            services.AddDbContext<SharpDiffDbContext>(options =>
                {
                    if (isTestingEnvironment)
                    {
                        options.UseInMemoryDatabase("TestingDB");
                    }
                    else
                    {
                        options.UseSqlServer(connString).EnableSensitiveDataLogging(true);
                    }
                });
        }

        /// <summary> Adds HealthCheck service and configure a self check and SqlServer check. </summary>
        /// <param name="services">IServiceCollection dependency</param>
        /// <param name="connString">ConnectionString.</param>
        public static void AddCustomHealthCheck(this IServiceCollection services, string connString)
        {
            var hcBuilder = services.AddHealthChecks();
            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            hcBuilder.AddSqlServer(connString);
        }

        /// <summary> Adds MVC Core services and configure. </summary>
        /// <param name="services">IServiceCollection dependency</param>
        public static void AddCustomMvcCore(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                // Apply filter globally to force response in JSON format.
                options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
            })
            .AddJsonOptions(options =>
            {
                // Suppress properties with null value.
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                // Converts an Enum to and from its name string value.
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            })
            .SetCompatibilityVersion(CompatibilityVersion.Latest)
            .AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.AddRouting(options => options.LowercaseUrls = true);
        }

        /// <summary> Adds API Versioning service and configure. </summary>
        /// <param name="services">IServiceCollection dependency</param>
        public static void AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                // Format of the version added to the route URL - "'v'major[.minor][-status]".
                options.GroupNameFormat = "'v'VVV";

                // Required for versioning by url segment and control the format of the API version in route templates.
                // Also, tells Swagger to replace the version in the controller route.
                options.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
