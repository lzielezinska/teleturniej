using Microsoft.EntityFrameworkCore;
using QuizApp.Models;

namespace QuizApp.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext (DbContextOptions<QuizDbContext> options)
            : base(options)
        {
        }

    public DbSet<Question> Question { get; set; }
    public DbSet<Answer> Answer { get; set; }
    }
}
