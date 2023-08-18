using Xunit;
using System.Net.Http;
using System.Net;
using TMDBLibrary.Services;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using TMDBTests;
namespace TMDBTests
{
    public class TMDBServiceTests
    {
        [Fact]
        public async Task GetPopularMovies_ReturnsMovies()
        {
            // Arrange
            var mockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{ \"Page\": 1, \"Results\": [ {\"Id\": 1, \"Title\": \"Test Movie\", \"Overview\": \"Test overview\", \"ReleaseDate\": \"2023-01-01\"} ] }")
            };

            var mockHttpClient = new HttpClient(new MockHttpHandler(mockResponse));

            var mockLogger = new Mock<ILogger<TMDBService>>();
            var service = new TMDBService(mockHttpClient, mockLogger.Object);

            // Act
            var movies = await service.GetPopularMovies();

            // Assert
            Assert.NotNull(movies);
            Assert.Single(movies);
            Assert.Equal("Test Movie", movies.First().Title);
        }

        [Fact]
        public async Task GetPopularMovies_ReturnsEmptyList_WhenNoMoviesPresent()
        {
            // Arrange
            var mockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{ \"Page\": 1, \"Results\": [] }")
            };

            var mockHttpClient = new HttpClient(new MockHttpHandler(mockResponse));
            var service = CreateTMDBService(mockHttpClient);

            // Act
            var movies = await service.GetPopularMovies();

            // Assert
            Assert.NotNull(movies);
            Assert.Empty(movies);
        }

        [Fact]
        public async Task GetPopularMovies_ThrowsException_OnErrorResponse()
        {
            // Arrange
            var mockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            };

            var mockHttpClient = new HttpClient(new MockHttpHandler(mockResponse));
            var service = CreateTMDBService(mockHttpClient);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => service.GetPopularMovies());
        }

        [Fact]
        public async Task GetPopularMovies_ReturnsEmptyList_OnUnexpectedJson()
        {
            // Arrange
            var mockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{ \"unexpectedKey\": \"unexpectedValue\" }")
            };

            var mockHttpClient = new HttpClient(new MockHttpHandler(mockResponse));
            var service = CreateTMDBService(mockHttpClient);

            // Act
            var movies = await service.GetPopularMovies();

            // Assert
            Assert.NotNull(movies);
            Assert.Empty(movies);
        }

        private TMDBService CreateTMDBService(HttpClient httpClient)
        {
            var mockLogger = new Mock<ILogger<TMDBService>>();
            return new TMDBService(httpClient, mockLogger.Object);
        }
    }
}