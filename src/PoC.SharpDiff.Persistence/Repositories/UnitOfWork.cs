using PoC.SharpDiff.Domain.Repositories;
using PoC.SharpDiff.Persistence.Contexts;
using System.Threading.Tasks;

namespace PoC.SharpDiff.Persistence.Repositories
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
