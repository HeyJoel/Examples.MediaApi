using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Examples.MediaApi.Domain.Tests
{
    public class SearchAlbumsQueryHandlerTests
    {
        const int DEFAULT_NUM_ALBUMS = 89;
        const int DEFAULT_NUM_USERS = 11;
        const int DEFAULT_PHOTOS_PER_ALBUM = 7;

        [Fact]
        public async Task Execute_WhenEmptyQuery_ReturnsAllAlbums()
        {
            var mediaApi = MockMediaRepository.CreateWithData(
                DEFAULT_NUM_ALBUMS,
                DEFAULT_NUM_USERS,
                DEFAULT_PHOTOS_PER_ALBUM
                );

            var query = new SearchAlbumsQuery();
            var handler = new SearchAlbumsQueryHandler(mediaApi, NullLogger<SearchAlbumsQueryHandler>.Instance);

            var result = await handler.ExecuteAsync(query);

            Assert.Equal(DEFAULT_NUM_ALBUMS, result.Count);
        }

        [Fact]
        public async Task Execute_WhenEmptyQuery_MapsPhotosToAlbums()
        {
            var mediaApi = MockMediaRepository.CreateWithData(
                DEFAULT_NUM_ALBUMS,
                DEFAULT_NUM_USERS,
                DEFAULT_PHOTOS_PER_ALBUM
                );

            var query = new SearchAlbumsQuery();
            var handler = new SearchAlbumsQueryHandler(mediaApi, NullLogger<SearchAlbumsQueryHandler>.Instance);

            var result = await handler.ExecuteAsync(query);
            var albumsWithCorrectNumberOfPhotos = result
                .Where(a => a.Photos.Count == DEFAULT_PHOTOS_PER_ALBUM);

            Assert.Equal(DEFAULT_NUM_ALBUMS, albumsWithCorrectNumberOfPhotos.Count());
        }

        [Fact]
        public async Task Execute_WithUserId_FiltersToUser()
        {
            const int USER_ID = 3;

            var mediaApi = MockMediaRepository.CreateWithData(
                DEFAULT_NUM_ALBUMS,
                DEFAULT_NUM_USERS,
                DEFAULT_PHOTOS_PER_ALBUM
                );

            var query = new SearchAlbumsQuery()
            {
                UserId = USER_ID
            };

            var handler = new SearchAlbumsQueryHandler(mediaApi, NullLogger<SearchAlbumsQueryHandler>.Instance);

            var result = await handler.ExecuteAsync(query);
            var albumsPerUser = result.GroupBy(a => a.UserId);
            var firstGroup = albumsPerUser.FirstOrDefault();

            Assert.NotNull(firstGroup);
            Assert.Single(albumsPerUser);
            Assert.Equal(USER_ID, firstGroup.Key);
        }

        [Fact]
        public async Task Execute_WithOutOfOrderTitles_IsOrderedByTitle()
        {
            const string FIRST_TITLE = "Aardvarks";
            const string LAST_TITLE = "Zoology";

            var mediaApi = new MockMediaRepository();
            mediaApi.AddAlbum(1, "Good times", 1);
            mediaApi.AddAlbum(2, LAST_TITLE, 1);
            mediaApi.AddAlbum(3, FIRST_TITLE, 1);

            var query = new SearchAlbumsQuery();
            var handler = new SearchAlbumsQueryHandler(mediaApi, NullLogger<SearchAlbumsQueryHandler>.Instance);

            var result = await handler.ExecuteAsync(query);

            var firstTitle = result
                .Select(a => a.Title)
                .FirstOrDefault();

            var lastTitle = result
                .Select(a => a.Title)
                .Skip(2)
                .FirstOrDefault();

            Assert.Equal(FIRST_TITLE, firstTitle);
            Assert.Equal(LAST_TITLE, lastTitle);
        }
    }
}
