using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PoC.SharpDiff.Persistence.Contexts;

namespace PoC.SharpDiff.WebAPI.Infrastructure.Extensions
{
    /// <summary> WebHost Extensions </summary>
    public static class WebHostExtensions
    {
        /// <summary> Ensure that all migrations were applied. </summary>
        /// <typeparam name="T" cref="SharpDiffDbContext">SharpDiff DbContext</typeparam>
        /// <param name="webHost" cref="IWebHost">IWebHost dependency</param>
        /// <returns>WebHost</returns>
        public static IWebHost MigrateDatabase<T>(this IWebHost webHost) where T : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<T>();
                    if (!db.AllMigrationsApplied())
                    {
                        db.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            return webHost;
        }

        /// <summary> Checks if has any remaining migration to apply </summary>
        /// <param name="context" cref="SharpDiffDbContext">SharpDiff DbContext</param>
        /// <returns>'false' if has migrations to apply, 'true' if all migrations applied.</returns>
        private static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }
    }
}
