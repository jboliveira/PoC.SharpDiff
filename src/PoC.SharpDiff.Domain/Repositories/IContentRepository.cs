using System.Threading.Tasks;
using PoC.SharpDiff.Domain.Models;

namespace PoC.SharpDiff.Domain.Repositories
{
    public interface IContentRepository
    {
        Task AddAsync(Content content);
        void Update(Content content);
        Task<Content> GetContentAsync(int id, ContentDirection direction);
    }
}
