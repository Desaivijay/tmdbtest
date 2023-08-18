using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDBTests
{
    using Xunit;
    using TMDBLibrary.Models;
    using System.ComponentModel.DataAnnotations;

    public class MovieTests
    {
        [Fact]
        public void CanCreateMovie()
        {
            var movie = new Movie
            {
                Id = 1,
                Title = "Test Movie",
                Description = "This is a test description.",
                ReleaseDate = new DateTime(2023, 1, 1)
            };

            Assert.NotNull(movie);
            Assert.Equal(1, movie.Id);
            Assert.Equal("Test Movie", movie.Title);
            Assert.Equal("This is a test description.", movie.Description);
            Assert.Equal(new DateTime(2023, 1, 1), movie.ReleaseDate);
        }

        [Fact]
        public void MovieTitleIsRequired()
        {
            var movie = new Movie
            {
                Description = "Description without title.",
                ReleaseDate = DateTime.Now
            };

            var context = new ValidationContext(movie) { MemberName = "Title" };
            var result = new List<ValidationResult>();
            bool isValid = Validator.TryValidateProperty(movie.Title, context, result);

            Assert.False(isValid);
            Assert.Single(result);
        }

        [Fact]
        public void MovieDescriptionIsRequired()
        {
            var movie = new Movie
            {
                Title = "Title without description.",
                ReleaseDate = DateTime.Now
            };

            var context = new ValidationContext(movie) { MemberName = "Description" };
            var result = new List<ValidationResult>();
            bool isValid = Validator.TryValidateProperty(movie.Description, context, result);

            Assert.False(isValid);
            Assert.Single(result);
        }

        [Fact]
        public void MovieDescriptionHasMaximumStringLength()
        {
            var movie = new Movie
            {
                Title = "Test Movie",
                Description = new string('a', 101),
                ReleaseDate = DateTime.Now
            };

            var context = new ValidationContext(movie) { MemberName = "Description" };
            var result = new List<ValidationResult>();
            bool isValid = Validator.TryValidateProperty(movie.Description, context, result);

            Assert.False(isValid);
            Assert.Single(result);
        }

        [Fact]
        public void MovieDescriptionIsValidWith100Characters()
        {
            var movie = new Movie
            {
                Title = "Test Movie",
                Description = new string('a', 100),
                ReleaseDate = DateTime.Now
            };

            var context = new ValidationContext(movie) { MemberName = "Description" };
            var result = new List<ValidationResult>();
            bool isValid = Validator.TryValidateProperty(movie.Description, context, result);

            Assert.True(isValid);
        }
    }

}
