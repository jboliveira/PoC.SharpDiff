using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoC.SharpDiff.Domain.Models;

namespace PoC.SharpDiff.Persistence.Mappings
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
            builder.HasKey(c => new { c.Id });

            builder.HasIndex(c => new { c.Id }).IsUnique();
            builder.HasIndex(c => new { c.Id, c.LeftContentData });
            builder.HasIndex(c => new { c.Id, c.RightContentData });

            builder.ToTable(nameof(Content));
        }
    }
}
