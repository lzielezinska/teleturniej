using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizApp.Models;

namespace QuizApp.Data
{
    public class QuizDbContext : IdentityDbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options)
            : base(options)
        { }

        public QuizDbContext()
        { }

        public DbSet<Question> Question { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<Attempt> Attempt { get; set; }
        public DbSet<Result> Result { get; set; }
    }
}
