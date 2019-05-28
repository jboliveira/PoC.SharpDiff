using Microsoft.EntityFrameworkCore;
using PoC.SharpDiff.Domain.Models;
using PoC.SharpDiff.Domain.Repositories;
using PoC.SharpDiff.Persistence.Contexts;
using System.Threading.Tasks;

namespace PoC.SharpDiff.Persistence.Repositories
{
    /// <summary>
    /// Content repository.
    /// </summary>
    public class ContentRepository : BaseRepository, IContentRepository
	{
		public ContentRepository(SharpDiffDbContext context) : base(context) { }

		/// <summary>
		/// Create a new Content.
		/// </summary>
		/// <param name="content">Content.</param>
		public async Task AddAsync(Content content)
		{
			await _context.Contents.AddAsync(content);
		}

		/// <summary>
		/// Update the specified content.
		/// </summary>
		/// <param name="content">Content.</param>
		public void Update(Content content)
		{
			_context.Contents.Update(content);
		}

		/// <summary>
		/// Gets the specified content.
		/// </summary>
		/// <returns cref="Content">The content.</returns>
		/// <param name="id">Content Identifier.</param>
		/// <param name="direction">Direction.</param>
		public async Task<Content> GetContentAsync(int id, ContentDirection direction)
		{
			return await _context.Contents.FirstOrDefaultAsync(c => c.Id == id && c.Direction == direction);
		}
	}
}
