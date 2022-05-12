using GamesShop.Catalog.API.ServiceExtensions;
using GamesShop.Catalog.BLL.Services.CategoryService;
using GamesShop.Catalog.DAL.Contexts;
using GamesShop.Catalog.DAL.Core;
using GamesShop.Catalog.DAL.Repositories.CategoryRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.LoadConfigurations();

builder.Services.AddScoped<ICatalogMongoDBContext, CatalogMongoDBContext>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();