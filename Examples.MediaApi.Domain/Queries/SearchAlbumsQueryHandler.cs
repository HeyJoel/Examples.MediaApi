using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examples.MediaApi.Domain
{
    /// <summary>
    /// Handles the execution of the SearchAlbumsQuery.
    /// </summary>
    public class SearchAlbumsQueryHandler
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly ILogger<SearchAlbumsQueryHandler> _logger;

        public SearchAlbumsQueryHandler(
            IMediaRepository mediaRepository,
            ILogger<SearchAlbumsQueryHandler> logger
            )
        {
            _mediaRepository = mediaRepository;
            _logger = logger;
        }

        public async Task<ICollection<Album>> ExecuteAsync(SearchAlbumsQuery query)
        {
            _logger.LogInformation("Executing query {QueryType}", typeof(SearchAlbumsQuery).Name);

            if (query == null) throw new ArgumentNullException();

            var albumTask = _mediaRepository.GetAlbumsAsync();
            var photoTask = _mediaRepository.GetPhotosAsync();

            await Task.WhenAll(albumTask, photoTask);

            var albums = await albumTask;
            var photos = await photoTask;

            var filteredAlbums = Filter(albums, query);
            var mappedResult = Map(filteredAlbums, photos);

            _logger.LogDebug("Found {NumAlbums} albums", nameof(mappedResult.Count));

            return mappedResult;
        }

        private IEnumerable<Album> Filter(ICollection<Album> albums, SearchAlbumsQuery query)
        {
            if (query.UserId.HasValue)
            {
                if (query.UserId < 1)
                {
                    // Depending on requirements you may wish to treat 0 as empty or throw a
                    // custom validation exception that can be caught and handled in the api 
                    // layer, or use model validation/attributes on the query
                    throw new Exception("Invalid UserId. UserId cannot be less than 1.");
                }

                _logger.LogDebug("Filtering by userid {UserId}", nameof(query.UserId));
                return albums.Where(a => a.UserId == query.UserId);
            }

            return albums;
        }

        private ICollection<Album> Map(IEnumerable<Album> albums, ICollection<Photo> photos)
        {
            foreach (var album in albums)
            {
                // No ordering specified, but let's assume ids give 
                // us a somewhat chronological order.
                album.Photos = photos
                    .Where(p => p.AlbumId == album.Id)
                    .OrderBy(p => p.Id)
                    .ToArray();
            }

            // apply consistent ordering - let's assume title ordering
            return albums
                .OrderBy(a => a.Title)
                .ToArray();
        }
    }
}
