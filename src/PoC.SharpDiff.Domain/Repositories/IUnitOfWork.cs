using System.Threading.Tasks;

namespace PoC.SharpDiff.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
