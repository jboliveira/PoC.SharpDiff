using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public DiffController(IContentService contentService)
        {
            _contentService = contentService ?? throw new ArgumentNullException(nameof(contentService));
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
            if (resource == null)
            {
                return BadRequest("'Resource' should not be null.");
            }

            var content = new Content(id);
            content.SetLeftContent(resource.Data);

            var result = await _contentService.UpsertContentAsync(content, ContentDirection.Left);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

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
            if (resource == null)
            {
                return BadRequest("'Resource' should not be null.");
            }

            var content = new Content(id);
            content.SetRightContent(resource.Data);

            var result = await _contentService.UpsertContentAsync(content, ContentDirection.Right);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

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
            var result = await _contentService.GetContentAsync(id);
            if (!result.Success)
            {
                return new NotFoundObjectResult(result.Message);
            }

            if (string.IsNullOrEmpty(result.Content.LeftContentData))
            {
                return new NotFoundObjectResult($"Content {id} left not found.");
            }

            if (string.IsNullOrEmpty(result.Content.RightContentData))
            {
                return new NotFoundObjectResult($"Content {id} right not found.");
            }

            var contentLeft = Convert.FromBase64String(result.Content.LeftContentData);
            var contentRight = Convert.FromBase64String(result.Content.RightContentData);

            var compareResult = _contentService.CompareContents(contentLeft, contentRight);
            if (compareResult.Differences?.Count > 0)
            {
                return new OkObjectResult(compareResult.Differences);
            }

            return new OkObjectResult(compareResult.Message);
        }
    }
}
