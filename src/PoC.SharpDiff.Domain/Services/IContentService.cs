using System.Threading.Tasks;
using PoC.SharpDiff.Domain.Models;
using PoC.SharpDiff.Domain.Services.Responses;

namespace PoC.SharpDiff.Domain.Services
{
    public interface IContentService
    {
        Task<ContentResponse> UpsertContentAsync(Content content, ContentDirection direction);
        Task<ContentResponse> GetContentAsync(int id);
        ContentDiffResponse CompareContents(byte[] contentLeft, byte[] contentRight);
    }
}
