using ExpenseService.Data;
using ExpenseService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IExpenseRepo, ExpenseRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICategoryDataClient, CategoryDataClient>();

builder.Services.AddControllers();

var app = builder.Build();

Seed.PrepPopulation(app);

app.UseAuthorization();

app.MapControllers();

app.Run();