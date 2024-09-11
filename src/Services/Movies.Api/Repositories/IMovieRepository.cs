using Movies.Api.Entities.Models;

namespace Movies.Api.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie?> GetMovieByIdAsync(Guid id);

        Task<Movie> CreateMovieAsync(Movie movie);
    }
}
