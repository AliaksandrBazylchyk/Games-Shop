using GamesShop.Gateway.ServiceExtensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configuration loader 
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", true, true)
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables()
    .Build();

// Getting variables
var identityServerConnectionString = configuration.GetValue<string>("identityServerConnectionString");

// Services loader
builder.Services.AddRouting();
builder.Services.AddOcelot();
builder.Services.AddIdentityServerAuthentication(identityServerConnectionString);
builder.Services.AddConfiguredCors();


var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.UseOcelot();

// TODO Add RabbitMQ
app.Run();