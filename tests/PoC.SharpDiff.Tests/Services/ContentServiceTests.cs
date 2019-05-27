using Moq;
using PoC.SharpDiff.WebAPI.Domain.Repositories;
using PoC.SharpDiff.WebAPI.Services;
using System.Linq;
using Xunit;

namespace PoC.SharpDiff.Tests.Services
{
    [Trait("Category", "Unit")]
    public class ContentServiceTests
    {
        private ContentService _sutContentService;

        public ContentServiceTests()
        {
            var stubContentRepository = new Mock<IContentRepository>();
            var stubUnitOfWork = new Mock<IUnitOfWork>();

            _sutContentService = new ContentService(stubContentRepository.Object, stubUnitOfWork.Object);
        }

        [Fact]
        public void CompareDataContent_AreEqualWithNoDifferences()
        {
            // Arrange
            var hello = new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f };

            // Act
            var result = _sutContentService.CompareContents(hello, hello);

            // Assert
            Assert.Equal("Content left and right are equal.", result.Message);
            Assert.Null(result.Differences);
        }

        [Fact]
        public void CompareDataContent_AreNotEqualInSize()
        {
            // Arrange
            var hello = new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f };
            var helloWorld = new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x2c, 0x20, 0x77, 0x6f, 72, 0x6c, 0x64 };

            // Act
            var result = _sutContentService.CompareContents(hello, helloWorld);

            // Assert
            Assert.Equal("Content left and right contains different sizes.", result.Message);
        }

        [Fact]
        public void CompareDataContent_WithDifferences()
        {
            // Arrange
            var hello = new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f };
            var herro = new byte[] { 0x48, 0x65, 0x72, 0x72, 0x6f };

            // Act
            var result = _sutContentService.CompareContents(hello, herro);

            // Assert
            var diff = result.Differences.FirstOrDefault();
            Assert.NotNull(result.Differences);
            Assert.NotNull(diff);
            Assert.True(diff.Offset == 2);
            Assert.True(diff.Length == 2);
        }
    }
}
