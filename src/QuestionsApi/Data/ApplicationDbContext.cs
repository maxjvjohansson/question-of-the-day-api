using Microsoft.EntityFrameworkCore;
using QuestionsApi.Models;

namespace QuestionsApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Question> Questions { get; set; } = null!;
    }
}