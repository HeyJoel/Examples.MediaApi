using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Examples.MediaApi.Domain.Tests
{
    public class MockMediaRepository : IMediaRepository
    {
        private List<Album> _albums = new List<Album>();
        private List<Photo> _photos = new List<Photo>();

        public Task<ICollection<Album>> GetAlbumsAsync()
        {
            return Task.FromResult<ICollection<Album>>(_albums);
        }

        public Task<ICollection<Photo>> GetPhotosAsync()
        {
            return Task.FromResult<ICollection<Photo>>(_photos);
        }

        public MockMediaRepository AddPhoto(int id, int albumId)
        {
            return AddPhoto(id, "Photo " + 1, albumId);
        }

        public MockMediaRepository AddPhoto(int id, string title, int albumId)
        {
            _photos.Add(new Photo()
            {
                Id = id,
                Title = title,
                AlbumId = albumId
            });

            return this;
        }

        public MockMediaRepository AddAlbum(int id, int userId)
        {
            return AddAlbum(id, "Album " + 1, userId);
        }

        public MockMediaRepository AddAlbum(int id, string title, int userId)
        {
            _albums.Add(new Album()
            {
                Id = id,
                Title = title,
                UserId = userId
            });

            return this;
        }

        /// <summary>
        /// Creates a mock media respository with a set of basic seed data. The
        /// data is added with sequential ids.
        /// </summary>
        /// <param name="totalAlbums">The total number of albums to seed the respository with.</param>
        /// <param name="totalusers">
        /// The total number of user to seed the respository with. Albums are distributed to users
        /// evenly throughout the collection.
        /// </param>
        /// <param name="photosPerAlbum">The number of photos to seed each album with.</param>
        public static MockMediaRepository CreateWithData(
            int totalAlbums, 
            int totalusers,
            int photosPerAlbum
            )
        {
            var mediaRepository = new MockMediaRepository();

            int userId = 1;

            for (int albumId = 1; albumId <= totalAlbums; albumId++)
            {
                mediaRepository.AddAlbum(albumId, userId);

                for (int photoId = 1; photoId <= photosPerAlbum; photoId++)
                {
                    mediaRepository.AddPhoto(photoId, albumId);
                }

                // distribute users evenly throughout the colleciton
                if (userId > totalusers)
                {
                    userId = 1;
                }
                else
                {
                    userId++;
                }
            }

            return mediaRepository;
        }
    }
}
