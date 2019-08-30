using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examples.MediaApi.Domain
{
    /// <summary>
    /// A photo belongs to a single album.
    /// </summary>
    public class Photo
    {
        public int Id { get; set; }

        public int AlbumId { get; set; }

        public string Title { get; set; }

        public Uri Url { get; set; }

        public Uri ThumbnailUrl { get; set; }
    }
}
