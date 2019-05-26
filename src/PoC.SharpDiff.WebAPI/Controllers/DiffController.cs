using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PoC.SharpDiff.WebAPI.Controllers
{
	/// <summary> Diff Controller </summary>
	[ApiVersion("1.0")]
	[Route("v{version:apiVersion}/[controller]")]
	public class DiffController : ControllerBase
	{
		[HttpPost("{id}/left")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> CreateContentLeftAsync(int id, [FromBody] object resource)
		{
			return new OkObjectResult(null);
		}

		[HttpPost("{id}/right")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> CreateContentRightAsync(int id, [FromBody] object resource)
		{
			return new OkObjectResult(null);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> CompareAsync(int id)
		{
			return new OkObjectResult(null);
		}
	}
}
