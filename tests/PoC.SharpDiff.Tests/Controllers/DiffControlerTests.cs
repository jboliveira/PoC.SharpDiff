using Newtonsoft.Json;
using PoC.SharpDiff.Tests.TestUtilities;
using PoC.SharpDiff.Tests.TestUtilities.Builders;
using PoC.SharpDiff.WebAPI;
using PoC.SharpDiff.WebAPI.Domain.Models;
using PoC.SharpDiff.WebAPI.Resources;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PoC.SharpDiff.Tests.Controllers
{
    [Trait("Category", "Integration")]
    public class DiffControlerTest : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        public DiffControlerTest(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task Should_Return_Success_When_Creates_Correct_Content_Left()
        {
            // Arrange
            const int id = 1;
            const ContentDirection direction = ContentDirection.Left;
            Content expectedContent = new ContentBuilder().WithId(id).WithDirection(direction).Build();
            CreateContentResource resource = new CreateContentResourceBuilder().WithData(expectedContent.Base64String).Build();

            // Act
            var response = await Client.PostAsync($"/v1/diff/{id}/left", resource.ToStringContent());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseAsString = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<Content>(responseAsString);

            Assert.Equal(id, content.Id);
            Assert.Equal(direction, content.Direction);
            Assert.Equal(expectedContent.Base64String, content.Base64String);
        }

        [Fact]
        public async Task Should_Return_Success_When_Creates_Correct_Content_Right()
        {
            // Arrange
            const int id = 1;
            const ContentDirection direction = ContentDirection.Right;
            Content expectedContent = new ContentBuilder().WithId(id).WithDirection(direction).Build();
            CreateContentResource resource = new CreateContentResourceBuilder().WithData(expectedContent.Base64String).Build();

            // Act
            var response = await Client.PostAsync($"/v1/diff/{id}/right", resource.ToStringContent());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseAsString = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<Content>(responseAsString);

            Assert.Equal(id, content.Id);
            Assert.Equal(direction, content.Direction);
            Assert.Equal(expectedContent.Base64String, content.Base64String);
        }
    }
}
