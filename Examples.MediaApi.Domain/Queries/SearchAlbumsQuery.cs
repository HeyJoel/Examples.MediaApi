using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examples.MediaApi.Domain
{
    /// <summary>
    /// Query parameters for the SearchAlbumsQueryHandler.
    /// </summary>
    public class SearchAlbumsQuery
    {
        /// <summary>
        /// Optional id of the user to filter on.
        /// </summary>
        public int? UserId { get; set; }
    }
}
