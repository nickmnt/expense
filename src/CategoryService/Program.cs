using CategoryService.Data;
using CategoryService.Models;
using CategoryService.SyncDataServices.Grpc;
using MongoDB.Driver;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddGrpc();
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<GrpcCategoryService>();

app.MapGet("/protos/platforms.proto", async context =>
{
    await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto"));
});

try
{
    await DbInitializer.InitDb(app);
}
catch (Exception ex)
{
    Console.Out.WriteLine($"--> DEBUG: ExpenseService error initializing DB: {ex.Message}");
}

app.Run();