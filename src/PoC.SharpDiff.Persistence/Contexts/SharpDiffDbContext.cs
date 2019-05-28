using Microsoft.EntityFrameworkCore;
using PoC.SharpDiff.Domain.Models;
using PoC.SharpDiff.Persistence.Mappings;

namespace PoC.SharpDiff.Persistence.Contexts
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
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ContentMap());

			base.OnModelCreating(modelBuilder);
		}
	}
}
