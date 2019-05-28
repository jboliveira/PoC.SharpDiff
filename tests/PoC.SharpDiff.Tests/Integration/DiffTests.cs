using Newtonsoft.Json;
using PoC.SharpDiff.Domain.Models;
using PoC.SharpDiff.Resources;
using PoC.SharpDiff.TestUtilities;
using PoC.SharpDiff.TestUtilities.Builders;
using PoC.SharpDiff.WebAPI;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PoC.SharpDiff.Tests
{
    [Trait("Category", "Integration")]
    public class DiffTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        public DiffTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        private async Task<HttpResponseMessage> CreateContentAsync(Content content)
        {
            CreateContentResource resource = new CreateContentResourceBuilder().WithData(content.Base64String).Build();

            var url = $"/v1/diff/{content.Id}/{content.Direction}";
            var response = await Client.PostAsync(url, resource.ToStringContent());
            return response;
        }

        [Fact]
        public async Task Should_CreateLeftContent_Test()
        {
            // Arrange
            const int id = 1;
            const ContentDirection direction = ContentDirection.Left;
            Content expectedContent = new ContentBuilder().WithId(id).WithDirection(direction).Build();

            // Act
            var response = await CreateContentAsync(expectedContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseAsString = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<Content>(responseAsString);

            Assert.Equal(id, content.Id);
            Assert.Equal(direction, content.Direction);
            Assert.Equal(expectedContent.Base64String, content.Base64String);
        }

        [Fact]
        public async Task Should_CreateRightContent_Test()
        {
            // Arrange
            const int id = 1;
            const ContentDirection direction = ContentDirection.Right;
            Content expectedContent = new ContentBuilder().WithId(id).WithDirection(direction).Build();

            // Act
            var response = await CreateContentAsync(expectedContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseAsString = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<Content>(responseAsString);

            Assert.Equal(id, content.Id);
            Assert.Equal(direction, content.Direction);
            Assert.Equal(expectedContent.Base64String, content.Base64String);
        }

        [Fact]
        public async Task Should_ReturnBadRequestForInvalidBase64_Test()
        {
            // Arrange
            const int id = 1;
            const ContentDirection direction = ContentDirection.Right;
            const string base64String = "xyz";
            Content content = new ContentBuilder()
                .WithId(id)
                .WithDirection(direction)
                .WithBase64String(base64String)
                .Build();

            // Act
            var response = await CreateContentAsync(content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Should_ReturnMessageContentsAreEqual_Test()
        {
            // Arrange
            const int id = 1;
            Content content1 = new ContentBuilder().WithId(id).WithDirection(ContentDirection.Left).Build();
            Content content2 = new ContentBuilder().WithId(id).WithDirection(ContentDirection.Right).Build();

            await CreateContentAsync(content1);
            await CreateContentAsync(content2);

            // Act
            var response = await Client.GetAsync($"/v1/diff/{id}");
            response.EnsureSuccessStatusCode();

            // Assert
            var result = await response.Content.ReadAsStringAsync();
            var resultText = JsonConvert.DeserializeObject<string>(result);
            Assert.Equal("Content left and right are equal.", resultText);
        }

        [Fact]
        public async Task Should_ReturnMessageContentsWithDiffSize_Test()
        {
            // Arrange
            const int id = 1;
            Content content1 = new ContentBuilder()
                .WithId(id)
                .WithDirection(ContentDirection.Left)
                .WithBase64String("BilacSP".ConvertToBase64FromUTF8String())
                .Build();

            Content content2 = new ContentBuilder()
                .WithId(id)
                .WithDirection(ContentDirection.Right)
                .WithBase64String("SalvadorBahiaBrazil".ConvertToBase64FromUTF8String())
                .Build();

            await CreateContentAsync(content1);
            await CreateContentAsync(content2);

            // Act
            var response = await Client.GetAsync($"/v1/diff/{id}");
            response.EnsureSuccessStatusCode();

            // Assert
            var result = await response.Content.ReadAsStringAsync();
            var resultText = JsonConvert.DeserializeObject<string>(result);
            Assert.Equal("Content left and right contains different sizes.", resultText);
        }

        [Fact]
        public async Task Should_ReturnDifferences_Test()
        {
            // Arrange
            const int id = 1;
            Content content1 = new ContentBuilder()
                .WithId(id)
                .WithDirection(ContentDirection.Left)
                .WithBase64String("BilacSP".ConvertToBase64FromUTF8String())
                .Build();

            Content content2 = new ContentBuilder()
                .WithId(id)
                .WithDirection(ContentDirection.Right)
                .WithBase64String("BilakRP".ConvertToBase64FromUTF8String())
                .Build();

            await CreateContentAsync(content1);
            await CreateContentAsync(content2);

            // Act
            var response = await Client.GetAsync($"/v1/diff/{id}");
            response.EnsureSuccessStatusCode();

            // Assert
            var result = await response.Content.ReadAsStringAsync();
            var differences = JsonConvert.DeserializeObject<List<ContentDiff>>(result);
            Assert.True(differences.Count > 0);
        }
    }
}
