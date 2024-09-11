using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var envName = builder.Environment.EnvironmentName;

builder.Configuration.AddJsonFile(Path.Combine("configuration", $"ocelot.{envName}.json"));

services.AddOcelot();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseOcelot().Wait();

app.Run();
