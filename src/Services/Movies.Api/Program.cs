using Movies.Api.Repositories;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddSingleton<IConnectionMultiplexer>(option =>
   ConnectionMultiplexer.Connect(new ConfigurationOptions
   {
       EndPoints = { builder.Configuration.GetConnectionString("MoviesDb")! },
       AbortOnConnectFail = false
   }));

services.AddScoped<IMovieRepository, RedisMovieRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
