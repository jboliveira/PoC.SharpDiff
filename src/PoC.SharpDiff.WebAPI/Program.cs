using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PoC.SharpDiff.Persistence.Contexts;
using PoC.SharpDiff.WebAPI.Infrastructure.Extensions;
using Serilog;

namespace PoC.SharpDiff.WebAPI
{
    /// <summary> Application entry point </summary>
    class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

        /// <summary> Application entry point </summary>
        /// <param name="args">Arguments</param>
        private static int Main(string[] args)
        {
            Log.Logger = CreateLoggerConfig();

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", AppName);
                var host = BuildWebHost(args);

                Log.Information("Loading Db Context ({ApplicationContext})...", AppName);
                host.MigrateDatabase<SharpDiffDbContext>();

                Log.Information("Starting web host ({ApplicationContext})...", AppName);
                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static ILogger CreateLoggerConfig() =>
            new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .CreateLogger();

        private static IHost BuildWebHost(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.CaptureStartupErrors(true);
                webBuilder.UseStartup<Startup>();
                webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
            })
            // Add a new service provider configuration
            .UseDefaultServiceProvider((context, options) =>
            {
                options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                options.ValidateOnBuild = true;
            }).Build();
    }
}
