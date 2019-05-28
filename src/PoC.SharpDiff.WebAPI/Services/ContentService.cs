using PoC.SharpDiff.Domain.Models;
using PoC.SharpDiff.Domain.Repositories;
using PoC.SharpDiff.Domain.Services;
using PoC.SharpDiff.Domain.Services.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoC.SharpDiff.WebAPI.Services
{
    /// <summary>
    /// Service to handle Content operations
    /// </summary>
    public class ContentService : IContentService
    {
        private readonly IContentRepository _contentRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PoC.SharpDiff.WebAPI.Services.ContentService"/> class.
        /// </summary>
        /// <param name="contentRepository">Content repository.</param>
        /// <param name="unitOfWork">Unit of work.</param>
        public ContentService(IContentRepository contentRepository, IUnitOfWork unitOfWork)
        {
            _contentRepository = contentRepository ?? throw new ArgumentNullException(nameof(contentRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Upserts the content async.
        /// </summary>
        /// <returns>The content response async.</returns>
        /// <param name="content">Content.</param>
        public async Task<ContentResponse> UpsertContentAsync(Content content)
        {
            if (content == null)
            {
                return new ContentResponse($"'Content' should not be null.");
            }

            var result = await GetContentAsync(content.Id, content.Direction);

            try
            {
                if (result.Success)
                {
                    result.Content.Base64String = content.Base64String;
                    _contentRepository.Update(result.Content);
                }
                else
                {
                    await _contentRepository.AddAsync(content);
                }

                await _unitOfWork.CommitAsync();

                return new ContentResponse(content);
            }
            catch (Exception ex)
            {
                return new ContentResponse($"An error occurred when saving the content: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the content async.
        /// </summary>
        /// <returns>The content response async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="direction">Direction.</param>
        public async Task<ContentResponse> GetContentAsync(int id, ContentDirection direction)
        {
            var content = await _contentRepository.GetContentAsync(id, direction);

            if (content == null)
            {
                return new ContentResponse($"Content {direction} {id} not found.");
            }

            return new ContentResponse(content);
        }

        /// <summary>
        /// Compares the contents.
        /// </summary>
        /// <returns>The contents.</returns>
        /// <param name="contentLeft">Content left.</param>
        /// <param name="contentRight">Content right.</param>
        public ContentDiffResponse CompareContents(byte[] contentLeft, byte[] contentRight)
        {
            if (contentLeft.Length != contentRight.Length)
            {
                return new ContentDiffResponse("Content left and right contains different sizes.");
            }

            var differences = new List<ContentDiff>();

            for (int i = 0; i < contentLeft.Length; i++)
            {
                if (contentLeft[i] != contentRight[i])
                {
                    int j = i;

					while ((contentLeft[j] != contentRight[j]) && j++ < contentLeft.Length - 1) { }

                    differences.Add(new ContentDiff(i, j - i));

                    i = j;
                }
            }

            if (differences.Count > 0)
            {
                return new ContentDiffResponse(differences);
            }

            return new ContentDiffResponse("Content left and right are equal.");
        }
    }
}
