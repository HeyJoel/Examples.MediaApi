using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examples.MediaApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Examples.MediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly SearchAlbumsQueryHandler _searchAlbumsQueryHandler;

        public AlbumsController(SearchAlbumsQueryHandler searchAlbumsQueryHandler)
        {
            _searchAlbumsQueryHandler = searchAlbumsQueryHandler;
        }

        [HttpGet]
        public async Task<ICollection<Album>> Get([FromQuery] SearchAlbumsQuery query)
        {
            if (query == null)
            {
                query = new SearchAlbumsQuery();
            }

            var result = await _searchAlbumsQueryHandler.ExecuteAsync(query);

            return result;
        }
    }
}
