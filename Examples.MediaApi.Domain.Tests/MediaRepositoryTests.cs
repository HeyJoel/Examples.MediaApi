using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Examples.MediaApi.Domain.Tests
{
    public class MediaRepositoryTests
    {
        [Fact]
        public async Task GetAlbums_MapsData()
        {
            var handler = new MockHttpMessageHandler();
            handler.AddMockJsonResponse("/albums", MockJsonData.Albums());
            ICollection<Album> albums;

            using (var httpClient = new HttpClient(handler))
            {
                var mediaRepository = new MediaRepository(httpClient);
                albums = await mediaRepository.GetAlbumsAsync();
            }

            Assert.NotEmpty(albums);
            var lastItem = albums.Last();

            Assert.Equal(100, lastItem.Id);
            Assert.Equal("enim repellat iste", lastItem.Title);
            Assert.Equal(10, lastItem.UserId);
            Assert.Null(lastItem.Photos);
        }

        [Fact]
        public async Task GetPhotos_MapsData()
        {
            var handler = new MockHttpMessageHandler();
            handler.AddMockJsonResponse("/photos", MockJsonData.Photos());
            ICollection<Photo> photos;

            using (var httpClient = new HttpClient(handler))
            {
                var mediaRepository = new MediaRepository(httpClient);
                photos = await mediaRepository.GetPhotosAsync();
            }

            Assert.NotEmpty(photos);
            var lastItem = photos.Last();

            Assert.Equal(5000, lastItem.Id);
            Assert.Equal("error quasi sunt cupiditate voluptate ea odit beatae", lastItem.Title);
            Assert.Equal(new Uri("https://via.placeholder.com/600/6dd9cb"), lastItem.Url);
            Assert.Equal(new Uri("https://via.placeholder.com/150/6dd9cb"), lastItem.ThumbnailUrl);
            Assert.Equal(100, lastItem.AlbumId);
        }
    }
}
