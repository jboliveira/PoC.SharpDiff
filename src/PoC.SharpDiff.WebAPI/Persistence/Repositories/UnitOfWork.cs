using System.Threading.Tasks;
using PoC.SharpDiff.WebAPI.Domain.Repositories;
using PoC.SharpDiff.WebAPI.Persistence.Contexts;

namespace PoC.SharpDiff.WebAPI.Persistence.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly SharpDiffDbContext _context;

		public UnitOfWork(SharpDiffDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Commit.
		/// </summary>
		public async Task CommitAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
