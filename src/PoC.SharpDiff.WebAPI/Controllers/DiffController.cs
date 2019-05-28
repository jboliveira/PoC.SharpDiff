using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PoC.SharpDiff.Domain.Models;
using PoC.SharpDiff.Domain.Services;
using PoC.SharpDiff.Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoC.SharpDiff.WebAPI.Controllers
{
    /// <summary> Diff Controller </summary>
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class DiffController : ControllerBase
    {
        private readonly IContentService _contentService;
        private readonly ILogger<DiffController> _logger;

        public DiffController(IContentService contentService, ILogger<DiffController> logger)
        {
            _contentService = contentService ?? throw new ArgumentNullException(nameof(contentService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Creates the content for left side.
        /// </summary>
        /// <returns>The content.</returns>
        /// <param name="id">Content Identifier.</param>
        /// <param name="resource">Content Resource for left side.</param>
        [HttpPost("{id}/left")]
        [ProducesResponseType(typeof(Content), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateContentLeftAsync(int id, [FromBody] CreateContentResource resource)
        {
            var content = new Content(id, ContentDirection.Left, resource.Data);

            var result = await _contentService.UpsertContentAsync(content);

            if (!result.Success)
                return BadRequest(result.Message);

            return new OkObjectResult(result.Content);
        }

        /// <summary>
        /// Creates the content for right side.
        /// </summary>
        /// <returns>The content.</returns>
        /// <param name="id">Content Identifier.</param>
        /// <param name="resource">Content Resource for right side.</param>
        [HttpPost("{id}/right")]
        [ProducesResponseType(typeof(Content), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateContentRightAsync(int id, [FromBody] CreateContentResource resource)
        {
            var content = new Content(id, ContentDirection.Right, resource.Data);

            var result = await _contentService.UpsertContentAsync(content);

            if (!result.Success)
                return BadRequest(result.Message);

            return new OkObjectResult(result.Content);
        }

        /// <summary>
        /// Compare the specified content id.
        /// </summary>
        /// <returns>The compare.</returns>
        /// <param name="id">Content Identifier.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<ContentDiff>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CompareAsync(int id)
        {
            var resultLeft = await _contentService.GetContentAsync(id, ContentDirection.Left);
            if (!resultLeft.Success)
            {
                return new NotFoundObjectResult(resultLeft.Message);
            }

            var resultRight = await _contentService.GetContentAsync(id, ContentDirection.Right);
            if (!resultRight.Success)
            {
                return new NotFoundObjectResult(resultRight.Message);
            }

            var compareResult = _contentService.CompareContents(resultLeft.GetContentBinaryData(), resultRight.GetContentBinaryData());
            if (compareResult.Differences?.Count > 0)
            {
                return new OkObjectResult(compareResult.Differences);
            }

            return new OkObjectResult(compareResult.Message);
        }
    }
}
