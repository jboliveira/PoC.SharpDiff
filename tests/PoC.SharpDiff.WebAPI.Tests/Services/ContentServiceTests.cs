using Moq;
using PoC.SharpDiff.Domain.Models;
using PoC.SharpDiff.Domain.Repositories;
using PoC.SharpDiff.Domain.Services.Responses;
using PoC.SharpDiff.TestUtilities;
using PoC.SharpDiff.TestUtilities.Builders;
using PoC.SharpDiff.WebAPI.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PoC.SharpDiff.WebAPI.Tests
{
    [Trait("Category", "Unit")]
    public static class ContentServiceTests
    {
        [Fact]
        public static async Task UpsertContentAsync_ReturnsContentResponse_WhenContentIsNull()
        {
            var sutContentService = SetupContentService(null);

            var result = await sutContentService.UpsertContentAsync(null, ContentDirection.Left);

            Assert.IsType<ContentResponse>(result);
        }

        [Fact]
        public static async Task UpsertContentAsync_ReturnsContentResponseNotSuccess_WhenContentIsNull()
        {
            var sutContentService = SetupContentService(null);

            var result = await sutContentService.UpsertContentAsync(null, ContentDirection.Left);

            Assert.False(result.Success);
        }

        [Fact]
        public static async Task UpsertContentAsync_ReturnsContentResponseErrorMessage_WhenContentIsNull()
        {
            var sutContentService = SetupContentService(null);

            var result = await sutContentService.UpsertContentAsync(null, ContentDirection.Left);

            Assert.Equal("'Content' should not be null.", result.Message);
        }

        [Theory]
        [InlineData(1, ContentDirection.Left)]
        [InlineData(2, ContentDirection.Right)]
        public static async Task UpsertContentAsync_ReturnsContentResponse_WhenNewContentIsValid(int id, ContentDirection direction)
        {
            Content content = new ContentBuilder().WithId(id).Build();
            var sutContentService = SetupContentService(null);

            var result = await sutContentService.UpsertContentAsync(content, direction);

            Assert.IsType<ContentResponse>(result);
        }

        [Theory]
        [InlineData(1, ContentDirection.Left)]
        [InlineData(2, ContentDirection.Right)]
        public static async Task UpsertContentAsync_ReturnsContentResponseSuccess_WhenNewContentIsValid(int id, ContentDirection direction)
        {
            Content content = new ContentBuilder().WithId(id).Build();
            var sutContentService = SetupContentService(null);

            var result = await sutContentService.UpsertContentAsync(content, direction);

            Assert.True(result.Success);
        }

        [Theory]
        [InlineData(1, ContentDirection.Left)]
        [InlineData(2, ContentDirection.Right)]
        public static async Task UpsertContentAsync_ReturnsContentResponseWithContentInserted_WhenNewContentIsValid(int id, ContentDirection direction)
        {
            Content content = new ContentBuilder().WithId(id).Build();
            var sutContentService = SetupContentService(null);

            var result = await sutContentService.UpsertContentAsync(content, direction);

            Assert.Equal(content, result.Content);
        }

        [Fact]
        public static async Task UpsertContentAsync_ReturnsContentResponse_WhenUpdatingExistingContent()
        {
            Content content1 = new ContentBuilder().WithId(2).WithLeftContent("BilacSP".ConvertToBase64FromUTF8String()).Build();
            Content content2 = new ContentBuilder().WithId(2).WithLeftContent("BilacSPBrazil".ConvertToBase64FromUTF8String()).Build();
            var sutContentService1 = SetupContentService(null);
            var sutContentService2 = SetupContentService(content1);

            await sutContentService1.UpsertContentAsync(content1, ContentDirection.Left);
            var result = await sutContentService2.UpsertContentAsync(content2, ContentDirection.Left);

            Assert.IsType<ContentResponse>(result);
        }

        [Fact]
        public static async Task UpsertContentAsync_ReturnsContentResponseSuccess_WhenUpdatingExistingContent()
        {
            Content content1 = new ContentBuilder().WithId(2).WithLeftContent("BilacSP".ConvertToBase64FromUTF8String()).Build();
            Content content2 = new ContentBuilder().WithId(2).WithLeftContent("BilacSPBrazil".ConvertToBase64FromUTF8String()).Build();
            var sutContentService1 = SetupContentService(null);
            var sutContentService2 = SetupContentService(content1);

            await sutContentService1.UpsertContentAsync(content1, ContentDirection.Left);
            var result = await sutContentService2.UpsertContentAsync(content2, ContentDirection.Left);

            Assert.True(result.Success);
        }

        [Fact]
        public static async Task UpsertContentAsync_ReturnsContentResponseWithContentUpdated_WhenUpdatingExistingContent()
        {
            Content content1 = new ContentBuilder().WithId(2).WithLeftContent("BilacSP".ConvertToBase64FromUTF8String()).Build();
            Content content2 = new ContentBuilder().WithId(2).WithLeftContent("BilacSPBrazil".ConvertToBase64FromUTF8String()).Build();
            var sutContentService1 = SetupContentService(null);
            var sutContentService2 = SetupContentService(content1);

            await sutContentService1.UpsertContentAsync(content1, ContentDirection.Left);
            var result = await sutContentService2.UpsertContentAsync(content2, ContentDirection.Left);

            Assert.Equal(content2, result.Content);
        }

        [Theory]
        [InlineData(1, ContentDirection.Left)]
        [InlineData(2, ContentDirection.Right)]
        public static async Task UpsertContentAsync_ThrowExceptionContentResponse_WhenUnitOfWorkFail(int id, ContentDirection direction)
        {
            // Arrange
            Content content = new ContentBuilder().WithId(id).Build();
            var sutContentService = SetupContentServiceException(content);

            // Act
            var result = await sutContentService.UpsertContentAsync(content, direction);

            // Asset
            Assert.IsType<ContentResponse>(result);
        }

        [Theory]
        [InlineData(1, ContentDirection.Left)]
        [InlineData(2, ContentDirection.Right)]
        public static async Task UpsertContentAsync_ThrowExceptionContentResponseNotSuccess_WhenUnitOfWorkFail(int id, ContentDirection direction)
        {
            // Arrange
            Content content = new ContentBuilder().WithId(id).Build();
            var sutContentService = SetupContentServiceException(content);

            // Act
            var result = await sutContentService.UpsertContentAsync(content, direction);

            // Asset
            Assert.False(result.Success);
        }

        [Theory]
        [InlineData(1, ContentDirection.Left)]
        [InlineData(2, ContentDirection.Right)]
        public static async Task UpsertContentAsync_ThrowExceptionContentResponseErrorMessage_WhenUnitOfWorkFail(int id, ContentDirection direction)
        {
            // Arrange
            Content content = new ContentBuilder().WithId(id).Build();
            var sutContentService = SetupContentServiceException(content);

            // Act
            var result = await sutContentService.UpsertContentAsync(content, direction);

            // Asset
            Assert.Contains("An error occurred when saving the content", result.Message);
        }

        [Theory]
        [InlineData(1, ContentDirection.Left)]
        [InlineData(2, ContentDirection.Right)]
        public static async Task UpsertContentAsync_ThrowExceptionContentNull_WhenUnitOfWorkFail(int id, ContentDirection direction)
        {
            // Arrange
            Content content = new ContentBuilder().WithId(id).Build();
            var sutContentService = SetupContentServiceException(content);

            // Act
            var result = await sutContentService.UpsertContentAsync(content, direction);

            // Asset
            Assert.Null(result.Content);
        }

        [Theory]
        [InlineData(2, ContentDirection.Right, "BilacSPBrazil")]
        [InlineData(1, ContentDirection.Left, "BilacSP")]
        public static async Task GetContentAsync_ReturnsContentResponseWithContentFound_WhenLookingForExistingContent(int id, ContentDirection direction, string text)
        {
            // Arrange
            Content content = new ContentBuilder().WithId(id).WithLeftContent(text.ConvertToBase64FromUTF8String()).Build();
            if (direction == ContentDirection.Right)
            {
                content = new ContentBuilder().WithId(id).WithRightContent(text.ConvertToBase64FromUTF8String()).Build();
            }

            var sutContentService = SetupContentService(content);

            // Act
            var result = await sutContentService.GetContentAsync(id);

            // Assert
            Assert.Equal(result.Content, content);
        }

        [Theory]
        [InlineData(1, ContentDirection.Left)]
        [InlineData(2, ContentDirection.Right)]
        public static async Task GetContentAsync_ReturnsContentResponseErrorMessage_WhenLookingForContentThatDoesNotExists(int id, ContentDirection direction)
        {
            // Arrange
            var sutContentService = SetupContentService(null);

            // Act
            var result = await sutContentService.GetContentAsync(id);

            // Assert
            Assert.Contains($"Content {id} not found.", result.Message);
        }

        [Fact]
        public static void CompareDataContent_AreEqualWithNoDifferences()
        {
            // Arrange            
            var hello = new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f };
            var sutContentService = SetupContentService(null);

            // Act
            var result = sutContentService.CompareContents(hello, hello);

            // Assert
            Assert.Equal("Content left and right are equal.", result.Message);
            Assert.Null(result.Differences);
        }

        [Fact]
        public static void CompareDataContent_AreNotEqualInSize()
        {
            // Arrange   
            var hello = new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f };
            var helloWorld = new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x2c, 0x20, 0x77, 0x6f, 72, 0x6c, 0x64 };
            var sutContentService = SetupContentService(null);

            // Act
            var result = sutContentService.CompareContents(hello, helloWorld);

            // Assert
            Assert.Equal("Content left and right contains different sizes.", result.Message);
        }

        [Fact]
        public static void CompareDataContent_WithDifferences()
        {
            // Arrange            
            var hello = new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f };
            var herro = new byte[] { 0x48, 0x65, 0x72, 0x72, 0x6f };
            var sutContentService = SetupContentService(null);

            // Act
            var result = sutContentService.CompareContents(hello, herro);

            // Assert
            var diff = result.Differences.FirstOrDefault();
            Assert.NotNull(result.Differences);
            Assert.NotNull(diff);
            Assert.True(diff.Offset == 2);
            Assert.True(diff.Length == 2);
        }

        /// <summary>
        /// The setup content service.
        /// </summary>
        private static readonly Func<Content, ContentService> SetupContentService = (contentReturn) =>
        {
            var stubContentRepository = new Mock<IContentRepository>();
            Content stubReturn = contentReturn;
            stubContentRepository.Setup(s => s.GetContentAsync(It.IsAny<int>())).ReturnsAsync(stubReturn);

            var stubUnitOfWork = new Mock<IUnitOfWork>();

            return new ContentService(stubContentRepository.Object, stubUnitOfWork.Object);
        };

        /// <summary>
        /// The setup content service exception.
        /// </summary>
        private static readonly Func<Content, ContentService> SetupContentServiceException = (contentReturn) =>
        {
            var stubContentRepository = new Mock<IContentRepository>();
            Content stubReturn = contentReturn;
            stubContentRepository.Setup(s => s.GetContentAsync(It.IsAny<int>())).ReturnsAsync(stubReturn);

            var stubUnitOfWork = new Mock<IUnitOfWork>();
            stubUnitOfWork.Setup(s => s.CommitAsync()).Throws(new Exception());

            return new ContentService(stubContentRepository.Object, stubUnitOfWork.Object);
        };
    }
}
