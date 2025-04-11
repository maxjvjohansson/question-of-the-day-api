using Microsoft.EntityFrameworkCore;
using QuestionsApi.Data;

Console.WriteLine("Starting app...");
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDirectoryBrowser();

// Determine connection string
var rawConnection = builder.Environment.IsDevelopment()
    ? "Data Source=questions.db"
    : Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING")
      ?? throw new InvalidOperationException("❌ Missing connection string in production!");

Console.WriteLine("📡 Raw connection string: " + rawConnection);

// Add DbContext based on environment
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(rawConnection));
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(rawConnection));
}

// Build app
WebApplication app;
try
{
    Console.WriteLine("🚧 Building app...");
    app = builder.Build();
    Console.WriteLine("✅ App built!");
}
catch (Exception ex)
{
    Console.WriteLine("❌ Build failed: " + ex.Message);
    Console.WriteLine(ex.StackTrace);
    throw;
}

// Middleware setup
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

// Apply any pending migrations
try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    Console.WriteLine("🔁 Running migrations...");
    db.Database.Migrate();
    Console.WriteLine("✅ Migration done!");
}
catch (Exception ex)
{
    Console.WriteLine("❌ Migration error!");
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
}

app.Run();
