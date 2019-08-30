using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.MediaApi.Domain.Tests
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly Dictionary<string, string> _mockJsonResponses = new Dictionary<string, string>();

        public void AddMockJsonResponse(string path, string json)
        {
            _mockJsonResponses.Add(path, json);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage();

            if (_mockJsonResponses.ContainsKey(request.RequestUri.LocalPath))
            {
                var json = _mockJsonResponses[request.RequestUri.LocalPath];
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            else
            {
                response.StatusCode = HttpStatusCode.NotFound;
            }

            return Task.FromResult(response);
        }
    }
}
