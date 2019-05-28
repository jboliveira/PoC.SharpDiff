using PoC.SharpDiff.Persistence.Contexts;

namespace PoC.SharpDiff.Persistence.Repositories
{
    public abstract class BaseRepository
	{
		protected readonly SharpDiffDbContext _context;

		protected BaseRepository(SharpDiffDbContext context)
		{
			_context = context;
		}
	}
}
