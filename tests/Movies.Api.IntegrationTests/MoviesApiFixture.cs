using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Movies.Api.Repositories;
using StackExchange.Redis;
using System.Reflection.PortableExecutable;
using Testcontainers.Redis;

namespace Movies.Api.IntegrationTests
{
    // TODO: Broken fixture
    public class MoviesApiFixture : WebApplicationFactory<Program>
    {
        private readonly RedisContainer _redisContainer;

        public MoviesApiFixture()
        {
            _redisContainer = new RedisBuilder()
              .WithImage("redis:latest")
              .Build();

            _redisContainer.StartAsync().Wait();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<IConnectionMultiplexer>();

                services.AddSingleton<IConnectionMultiplexer>(option =>
                   ConnectionMultiplexer.Connect(new ConfigurationOptions
                   {
                       EndPoints = { _redisContainer.GetConnectionString() },
                       AbortOnConnectFail = false
                   }));
            });
        }
    }
}
