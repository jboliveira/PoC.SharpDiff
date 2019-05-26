using System.Threading.Tasks;
using PoC.SharpDiff.WebAPI.Domain.Models;

namespace PoC.SharpDiff.WebAPI.Domain.Repositories
{
	public interface IContentRepository
	{
		Task AddAsync(Content content);
		void Update(Content content);
		Task<Content> GetContentAsync(int id, ContentDirection direction);
	}
}
