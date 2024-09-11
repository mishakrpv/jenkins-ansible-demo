using Movies.Api.Entities.Models;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Movies.Api.Repositories
{
    public class RedisMovieRepository(ILogger<RedisMovieRepository> logger, IConnectionMultiplexer redis) : IMovieRepository
    {
        private readonly ILogger<RedisMovieRepository> _logger = logger;
        private readonly IDatabase _database = redis.GetDatabase();

        private static RedisKey _scopeKeyPrefix = (RedisKey)"/movie/"u8.ToArray();
        private static RedisKey GetScopeKey(string id) => _scopeKeyPrefix.Append(id);

        public async Task<Movie> CreateMovieAsync(Movie movie)
        {
            var json = JsonSerializer.SerializeToUtf8Bytes(movie, MovieSerializationContext.Default.Movie);
            await _database.StringSetAsync(GetScopeKey(movie.Id.ToString()), (RedisValue)json);

            return (await GetMovieByIdAsync(movie.Id))!;
        }

        public async Task<Movie?> GetMovieByIdAsync(Guid id)
        {
            using var data = await _database.StringGetLeaseAsync(GetScopeKey(id.ToString()));

            if (data is null || data.Length == 0)
            {
                return null;
            }

            return JsonSerializer.Deserialize(data.Span, MovieSerializationContext.Default.Movie);
        }
    }

    [JsonSerializable(typeof(Movie))]
    [JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
    public partial class MovieSerializationContext : JsonSerializerContext
    {

    }
}
