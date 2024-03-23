using CategoryService.Models;
using MongoDB.Driver;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

await DB.InitAsync("CategoryDb", MongoClientSettings
    .FromConnectionString(builder.Configuration.GetConnectionString("MongoDbConnection")));

await DB.Index<Category>()
    .Key(x => x.Id, type: KeyType.Ascending)
    .CreateAsync();

app.Run();