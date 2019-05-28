using System.Threading.Tasks;
using PoC.SharpDiff.Domain.Models;
using PoC.SharpDiff.Domain.Services.Responses;

namespace PoC.SharpDiff.Domain.Services
{
    public interface IContentService
    {
        Task<ContentResponse> UpsertContentAsync(Content content);
        Task<ContentResponse> GetContentAsync(int id, ContentDirection direction);
        ContentDiffResponse CompareContents(byte[] contentLeft, byte[] contentRight);
    }
}
