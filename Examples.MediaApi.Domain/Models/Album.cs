using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examples.MediaApi.Domain
{
    /// <summary>
    /// A photo album is owed by a specific user, and can 
    /// contain 0 or more images.
    /// </summary>
    public class Album
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
