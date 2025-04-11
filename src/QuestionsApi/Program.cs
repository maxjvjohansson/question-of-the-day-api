using Microsoft.EntityFrameworkCore;
using QuestionsApi.Data;

Console.WriteLine("Starting app...");
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDirectoryBrowser();

var rawConn = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
Console.WriteLine($"📡 RAW POSTGRES_CONNECTION_STRING: {rawConn}");

string connectionString;

if (builder.Environment.IsDevelopment())
{
    connectionString = "Data Source=questions.db";
}
else
{
    if (string.IsNullOrWhiteSpace(rawConn))
        throw new InvalidOperationException("❌ Missing production connection string");

    // Om det redan är i rätt format, använd direkt
    connectionString = rawConn.Contains("Host=") ? rawConn : ConvertPostgresConnectionString(rawConn);
}


string? ConvertPostgresConnectionString(string? connectionString)
{
    if (string.IsNullOrEmpty(connectionString))
    {
        return null;
    }
    
    if (connectionString.Contains("Host="))
    {
        return connectionString;
    }
    
    try
    {
        var uri = new Uri(connectionString);
        var userInfo = uri.UserInfo.Split(':');
        var host = uri.Host;
        var port = uri.Port;
        var database = uri.AbsolutePath.TrimStart('/');
        
        return $"Host={host};Port={port};Database={database};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true;";
    }
    catch
    {
        return connectionString;
    }
}

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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();
