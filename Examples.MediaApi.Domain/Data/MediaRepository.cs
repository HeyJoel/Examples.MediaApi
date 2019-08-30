using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Examples.MediaApi.Domain
{
    public class MediaRepository : IMediaRepository
    {
        private HttpClient _httpClient;

        public MediaRepository(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("http://jsonplaceholder.typicode.com/");
            _httpClient = httpClient;
        }

        public Task<ICollection<Album>> GetAlbumsAsync()
        {
            return GetAsync<Album>("albums");
        }

        public Task<ICollection<Photo>> GetPhotosAsync()
        {
            return GetAsync<Photo>("photos");
        }

        private async Task<ICollection<T>> GetAsync<T>(string path)
        {
            var response = await _httpClient.GetAsync(path);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<ICollection<T>>();

            return result;
        }
    }
}
