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
        private readonly HttpClient Client;

        public DiffTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        private async Task<HttpResponseMessage> CreateContentAsync(Content content, ContentDirection direction)
        {
            CreateContentResource resource = new CreateContentResourceBuilder().WithData(content.LeftContentData).Build();
            if(direction == ContentDirection.Right)
            {
                resource = new CreateContentResourceBuilder().WithData(content.RightContentData).Build();
            }

            var url = $"/v1/diff/{content.Id}/{direction}";
            var response = await Client.PostAsync(url, resource.ToStringContent());
            return response;
        }

        [Fact]
        public async Task Should_CreateLeftContent_Test()
        {
            // Arrange
            const int id = 1;
            const ContentDirection direction = ContentDirection.Left;
            Content expectedContent = new ContentBuilder().WithId(id).Build();

            // Act
            var response = await CreateContentAsync(expectedContent, direction).ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseAsString = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<Content>(responseAsString);

            Assert.Equal(id, content.Id);
            Assert.Equal(expectedContent.LeftContentData, content.LeftContentData);
        }

        [Fact]
        public async Task Should_CreateRightContent_Test()
        {
            // Arrange
            const int id = 1;
            const ContentDirection direction = ContentDirection.Right;
            Content expectedContent = new ContentBuilder().WithId(id).Build();

            // Act
            var response = await CreateContentAsync(expectedContent, direction).ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseAsString = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<Content>(responseAsString);

            Assert.Equal(id, content.Id);
            Assert.Equal(expectedContent.RightContentData, content.RightContentData);
        }

        [Fact]
        public async Task Should_ReturnBadRequestForInvalidBase64_Test()
        {
            // Arrange
            const int id = 1;
            const string base64String = "xyz";
            Content content = new ContentBuilder()
                .WithId(id)
                .WithLeftContent(base64String)
                .Build();

            // Act
            var response = await CreateContentAsync(content, ContentDirection.Left).ConfigureAwait(false);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Should_ReturnMessageContentsAreEqual_Test()
        {
            // Arrange
            const int id = 1;
            Content content1 = new ContentBuilder().WithId(id).Build();
            Content content2 = new ContentBuilder().WithId(id).Build();

            await CreateContentAsync(content1, ContentDirection.Left).ConfigureAwait(false);
            await CreateContentAsync(content2, ContentDirection.Right).ConfigureAwait(false);

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
                .WithLeftContent("BilacSP".ConvertToBase64FromUTF8String())
                .Build();

            Content content2 = new ContentBuilder()
                .WithId(id)
                .WithRightContent("SalvadorBahiaBrazil".ConvertToBase64FromUTF8String())
                .Build();

            await CreateContentAsync(content1, ContentDirection.Left).ConfigureAwait(false);
            await CreateContentAsync(content2, ContentDirection.Right).ConfigureAwait(false);

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
                .WithLeftContent("BilacSP".ConvertToBase64FromUTF8String())
                .Build();

            Content content2 = new ContentBuilder()
                .WithId(id)
                .WithRightContent("BilakRP".ConvertToBase64FromUTF8String())
                .Build();

            await CreateContentAsync(content1, ContentDirection.Left).ConfigureAwait(false);
            await CreateContentAsync(content2, ContentDirection.Right).ConfigureAwait(false);

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
