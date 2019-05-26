using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoC.SharpDiff.WebAPI.Domain.Models;

namespace PoC.SharpDiff.WebAPI.Persistence.Mappings
{
	/// <summary>
	/// Content map.
	/// </summary>
	public class ContentMap : IEntityTypeConfiguration<Content>
	{
		/// <summary>
		/// Configure the specified builder.
		/// </summary>
		/// <param name="builder">Content Builder.</param>
		public void Configure(EntityTypeBuilder<Content> builder)
		{
			builder.HasKey(c => new { c.Id, c.Direction });

			builder.HasIndex(c => new { c.Id, c.Direction });

			builder.ToTable(nameof(Content));
		}
	}
}
