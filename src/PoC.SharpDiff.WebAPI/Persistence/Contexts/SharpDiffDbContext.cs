using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PoC.SharpDiff.WebAPI.Domain.Models;
using PoC.SharpDiff.WebAPI.Persistence.Mappings;

namespace PoC.SharpDiff.WebAPI.Persistence.Contexts
{
	/// <summary>
	/// SharpDiff dbcontext.
	/// </summary>
	public class SharpDiffDbContext : DbContext
	{
		public DbSet<Content> Contents { get; set; }

		/// <inheritdoc />
		public SharpDiffDbContext(DbContextOptions<SharpDiffDbContext> options) : base(options)
		{ }

		/// <inheritdoc />
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder.IsConfigured) return;

			var configuration = new ConfigurationBuilder()
							.SetBasePath(Directory.GetCurrentDirectory())
							.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
							.Build();

			optionsBuilder.UseSqlServer(configuration.GetConnectionString("SharpDiffDatabase"));
		}

		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ContentMap());

			base.OnModelCreating(modelBuilder);
		}
	}
}
