using System.Threading.Tasks;

namespace PoC.SharpDiff.WebAPI.Domain.Repositories
{
	public interface IUnitOfWork
	{
		Task CommitAsync();
	}
}
