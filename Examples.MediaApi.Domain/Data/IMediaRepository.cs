using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Examples.MediaApi.Domain
{
    /// <summary>
    /// Simple abstraction over the album and photo
    /// APIs.
    /// </summary>
    public interface IMediaRepository
    {
        Task<ICollection<Album>> GetAlbumsAsync();

        Task<ICollection<Photo>> GetPhotosAsync();
    }
}
