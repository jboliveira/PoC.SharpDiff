using PoC.SharpDiff.WebAPI.Persistence.Contexts;

namespace PoC.SharpDiff.WebAPI.Persistence.Repositories
{
	public abstract class BaseRepository
	{
		protected readonly SharpDiffDbContext _context;

		public BaseRepository(SharpDiffDbContext context)
		{
			_context = context;
		}
	}
}
