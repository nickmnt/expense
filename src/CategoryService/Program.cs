using CategoryService.Data;
using CategoryService.Models;
using MongoDB.Driver;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

try
{
    await DbInitializer.InitDb(app);
}
catch (Exception ex)
{
    Console.Out.WriteLine($"--> DEBUG: ExpenseService error initializing DB: {ex.Message}");
}

app.Run();