using Examples.MediaApi.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Examples.MediaApi.Tests.Integration
{
    public class AlbumsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;

        public AlbumsControllerTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task Get_WithNoQuery_ReturnsResults()
        {
            var client = _webApplicationFactory.CreateClient();

            var response = await client.GetAsync("/api/albums");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var albums = JsonConvert.DeserializeObject<List<Album>>(json);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(albums);
        }

        [Fact]
        public async Task Get_WithUserId_ReturnsResults()
        {
            var client = _webApplicationFactory.CreateClient();

            var response = await client.GetAsync("/api/albums?userid=1");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var albums = JsonConvert.DeserializeObject<List<Album>>(json);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(albums);
        }
    }
}
