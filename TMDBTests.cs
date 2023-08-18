using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDBLibrary.Services;

namespace TMDBTests
{
    public class TMDBTests
    {
        [Fact]
        public void CanCreateTMDBResponse()
        {
            var response = new TMDBResponse
            {
                Page = 1,
                Results = new List<TMDBMovie>
            {
                new TMDBMovie
                {
                    Id = 1,
                    Title = "Test Movie",
                    Overview = "This is a test overview.",
                    ReleaseDate = new DateTime(2023, 1, 1)
                }
            }
            };

            Assert.NotNull(response);
            Assert.Equal(1, response.Page);
            Assert.Single(response.Results);
            Assert.Equal(1, response.Results[0].Id);
            Assert.Equal("Test Movie", response.Results[0].Title);
            Assert.Equal("This is a test overview.", response.Results[0].Overview);
            Assert.Equal(new DateTime(2023, 1, 1), response.Results[0].ReleaseDate);
        }

        [Fact]
        public void CanCreateTMDBMovie()
        {
            var movie = new TMDBMovie
            {
                Id = 1,
                Title = "Test Movie",
                Overview = "This is a test overview.",
                ReleaseDate = new DateTime(2023, 1, 1)
            };

            Assert.NotNull(movie);
            Assert.Equal(1, movie.Id);
            Assert.Equal("Test Movie", movie.Title);
            Assert.Equal("This is a test overview.", movie.Overview);
            Assert.Equal(new DateTime(2023, 1, 1), movie.ReleaseDate);
        }
    }
}
