using System.Net.Mime;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PoC.SharpDiff.Persistence.Contexts;

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
                        options.UseSqlServer(connString)
                            .EnableSensitiveDataLogging(true);
                    }
                });
        }

        /// <summary> Adds MVC Core services and configure. </summary>
        /// <param name="services">IServiceCollection dependency</param>
        public static void AddCustomControllers(this IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers()
            .AddMvcOptions(options =>
            {
                // Apply filter globally to force response in JSON format.
                options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
            })
            .AddJsonOptions(options =>
            {
                // Suppress properties with null value.
                options.JsonSerializerOptions.IgnoreNullValues = true;
                // Converts an Enum to and from its name string value.
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<Startup>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Latest)
            .AddControllersAsServices();
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
