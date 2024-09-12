using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Movies.Api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Movies.Api.Controllers.MoviesController;

namespace Movies.Api.IntegrationTests
{
    public class MoviesApiTests : IClassFixture<MoviesApiFixture>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new(JsonSerializerDefaults.Web);

        public MoviesApiTests(MoviesApiFixture fixture)
        {
            _factory = fixture;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task Create_Movie_ReturnsOk()
        {
            // Arrange
            var request = new CreateMovieRequest("Title", "Description");

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/movies", request);

            // Assert
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            var createdMovie = JsonSerializer.Deserialize<Movie>(body, _jsonSerializerOptions);

            createdMovie.Should().NotBeNull();
            createdMovie.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Query_MovieById_ReturnsOk()
        {
            // Arrange
            var movieId = Guid.NewGuid();

            // Act
            var response = await _httpClient.GetAsync($"/api/movies/{movieId}");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
