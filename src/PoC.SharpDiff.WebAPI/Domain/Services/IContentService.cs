using System.Threading.Tasks;
using PoC.SharpDiff.WebAPI.Domain.Models;
using PoC.SharpDiff.WebAPI.Domain.Services.Responses;

namespace PoC.SharpDiff.WebAPI.Domain.Services
{
	public interface IContentService
	{
		Task<ContentResponse> UpsertContentAsync(Content content);
		Task<ContentResponse> GetContentAsync(int id, ContentDirection direction);
		ContentDiffResponse CompareContents(byte[] contentLeft, byte[] contentRight);
	}
}
