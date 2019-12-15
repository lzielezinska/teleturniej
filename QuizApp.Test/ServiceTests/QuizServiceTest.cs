using System;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;
using QuizApp.Models.Services;
using Xunit;
using System.Linq;

namespace QuizApp.Test.ServiceTests
{
    public class QuizServiceTest
    {
        [Fact]
        public void Add_quiz_to_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_quiz_to_database")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new QuizService(context);
                Quiz example_quiz = new Quiz { Name = "AAA" };
                service.CreateQuiz(example_quiz);
                context.SaveChanges();
            }

            using (var context = new QuizDbContext(options))
            {
                Assert.Equal(1, context.Quiz.Count());
                Assert.Equal("AAA", context.Quiz.Single().Name);
            }
        }

        [Fact]
        public void Delete_quiz_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_quiz_from_database")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new QuizService(context);
                Quiz q1 = new Quiz { Name = "AAA", Id = 1 };
                Quiz q2 = new Quiz { Name = "BBB", Id = 2 };

                service.CreateQuiz(q1);
                service.CreateQuiz(q2);

                context.SaveChanges();
            }

            using (var context = new QuizDbContext(options))
            {
                var service = new QuizService(context);
                service.DeleteQuizByID(1);
                Assert.Equal(1, context.Quiz.Count());
                Assert.Equal("BBB", context.Quiz.Single().Name);
                Assert.Equal(2, context.Quiz.Single().Id);
            }
        }
        [Fact]
        public void Get_all_quizes_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_all_quizes_from_database")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new QuizService(context);
                Quiz q1 = new Quiz { Name = "AAA", Id = 1 };
                Quiz q2 = new Quiz { Name = "BBB", Id = 2 };

                service.CreateQuiz(q1);
                service.CreateQuiz(q2);

                context.SaveChanges();
            }

            using (var context = new QuizDbContext(options))
            {
                var service = new QuizService(context);
                var actual = service.GetAll();

                Assert.Equal("AAA", actual[0].Name);
                Assert.Equal("BBB", actual[1].Name);
                Assert.Equal(2, actual.Count());

            }
        }

        [Fact]
        public void Get_quiz_id_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_quiz_id_from_database")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new QuizService(context);
                Quiz q1 = new Quiz { Name = "AAA", Id = 1 };
                Quiz q2 = new Quiz { Name = "BBB", Id = 2 };

                service.CreateQuiz(q1);
                service.CreateQuiz(q2);

                context.SaveChanges();
            }

            using (var context = new QuizDbContext(options))
            {
                var service = new QuizService(context);
                var actual = service.GetQuizByID(1);

                Assert.Equal("AAA", actual.Name);
            }
        }

        
        [Fact]
        public void Update_quiz()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Update_quiz")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new QuizService(context);
                Quiz q1 = new Quiz { Name = "AAA", Id = 1 };
                Quiz q2 = new Quiz { Name = "BBB", Id = 2 };

                service.CreateQuiz(q1);
                service.CreateQuiz(q2);

                context.SaveChanges();
            }

            using (var context = new QuizDbContext(options))
            {
                var service = new QuizService(context);
                Quiz q1 = new Quiz { Name = "AAB",  Id = 1 };
                service.UpdateQuiz(q1);

                Assert.Equal(2, context.Quiz.Count());
                Assert.Equal("AAB", context.Quiz.FirstOrDefault(x => x.Id == 1).Name);
            }
        }
    }
}
