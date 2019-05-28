using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PoC.SharpDiff.Domain.Models;
using PoC.SharpDiff.Domain.Services;
using PoC.SharpDiff.Domain.Services.Responses;
using PoC.SharpDiff.Resources;
using PoC.SharpDiff.TestUtilities;
using PoC.SharpDiff.TestUtilities.Builders;
using PoC.SharpDiff.WebAPI.Controllers;
using Xunit;

namespace PoC.SharpDiff.WebAPI.Tests.Controllers
{
	[Trait("Category", "Unit")]
    public static class DiffControllerTests
    {
		[Theory]
		[InlineData(1, "BilacSPBrazil")]
		[InlineData(2, "SaoPauloSantaCatarina")]
		public static async Task CreateContentLeftAsync_ReturnsOkResult_WhenResourceIsValid(int id, string text)
		{
			// Arrange
			var data = text.ConvertToBase64FromUTF8String();
			CreateContentResource resource = new CreateContentResourceBuilder().WithData(data).Build();
			Content content = new ContentBuilder().WithId(id).WithDirection(ContentDirection.Left).WithBase64String(data).Build();
			ContentResponse response = new ContentResponse(content);
			var sutDiffController = SetupDiffControllerForUpsertContentAsync(response);

			// Act
			var result = await sutDiffController.CreateContentLeftAsync(id, resource);

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}

		[Theory]
		[InlineData(1, "BilacSPBrazil", "'Content' should not be null.")]
		public static async Task CreateContentLeftAsync_ReturnsBadRequestResult_WhenResourceIsInvalid(int id, string text, string expectedErrorMessage)
		{
			// Arrange
			var data = text.ConvertToBase64FromUTF8String();
			CreateContentResource resource = new CreateContentResourceBuilder().WithData(data).Build();
			ContentResponse response = new ContentResponse(expectedErrorMessage);
			var sutDiffController = SetupDiffControllerForUpsertContentAsync(response);

			// Act
			var result = await sutDiffController.CreateContentLeftAsync(id, resource);

			// Assert
			Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal(expectedErrorMessage, (result as BadRequestObjectResult).Value);
		}

		[Theory]
		[InlineData(1, "BilacSPBrazil")]
		[InlineData(2, "SaoPauloSantaCatarina")]
		public static async Task CreateContentRightAsync_ReturnsOkResult_WhenResourceIsValid(int id, string text)
		{
			// Arrange
			var data = text.ConvertToBase64FromUTF8String();
			CreateContentResource resource = new CreateContentResourceBuilder().WithData(data).Build();
			Content content = new ContentBuilder().WithId(id).WithDirection(ContentDirection.Right).WithBase64String(data).Build();
			ContentResponse response = new ContentResponse(content);
			var sutDiffController = SetupDiffControllerForUpsertContentAsync(response);

			// Act
			var result = await sutDiffController.CreateContentRightAsync(id, resource);

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}

		[Theory]
		[InlineData(1, "BilacSPBrazil", "'Content' should not be null.")]
		public static async Task CreateContentRightAsync_ReturnsBadRequestResult_WhenResourceIsInvalid(int id, string text, string expectedErrorMessage)
		{
			// Arrange
			var data = text.ConvertToBase64FromUTF8String();
			CreateContentResource resource = new CreateContentResourceBuilder().WithData(data).Build();
			ContentResponse response = new ContentResponse(expectedErrorMessage);
			var sutDiffController = SetupDiffControllerForUpsertContentAsync(response);

			// Act
			var result = await sutDiffController.CreateContentRightAsync(id, resource);

			// Assert
			Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal(expectedErrorMessage, (result as BadRequestObjectResult).Value);
		}

		/// <summary>
		/// The setup diff controller for upsert content async.
		/// </summary>
		private static readonly Func<ContentResponse, DiffController> SetupDiffControllerForUpsertContentAsync = (response) =>
		{
			var stubContentService = new Mock<IContentService>();
			ContentResponse stubReturn = response;
			stubContentService.Setup(s => s.UpsertContentAsync(It.IsAny<Content>())).ReturnsAsync(stubReturn);

			return new DiffController(stubContentService.Object);
		};
	}
}
