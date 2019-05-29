using PoC.SharpDiff.Domain.Models;
using System.Threading.Tasks;

namespace PoC.SharpDiff.Domain.Repositories
{
    public interface IContentRepository
    {
        Task AddAsync(Content content);
        void Update(Content content);
        Task<Content> GetContentAsync(int id);
    }
}
