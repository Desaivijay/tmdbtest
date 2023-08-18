using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;


namespace TMDBTests
{
    public class MockHttpHandler : DelegatingHandler
    {
        private readonly HttpResponseMessage _response;

        public MockHttpHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }
    }
}
