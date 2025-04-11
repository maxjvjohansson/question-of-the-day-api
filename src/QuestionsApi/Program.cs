using Microsoft.EntityFrameworkCore;
using QuestionsApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDirectoryBrowser();

string connectionString = builder.Environment.IsDevelopment()
    ? "Data Source=questions.db"
    : Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING")
    ?? throw new InvalidOperationException("Missing production connection string");

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
}

else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine("❌ Unhandled exception:");
    Console.WriteLine(ex.ToString());
}