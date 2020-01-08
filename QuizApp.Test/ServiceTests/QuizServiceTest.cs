using System;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;
using QuizApp.Models.Services;
using Xunit;
using System.Linq;
using FluentAssertions;


namespace QuizApp.Test.ServiceTests
{
    public class QuizServiceTest
    {
        private static void Add_example_recorods(DbContextOptions<QuizDbContext> options)
        {
            using (var context = new QuizDbContext(options))
            {
                context.Quiz.Add(new Quiz { Name = "AAA", Id = 1 });
                context.Quiz.Add(new Quiz { Name = "BBB", Id = 2 });
                context.SaveChanges();
            }
        }

        [Fact]
        public void Add_quiz_to_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_quiz_to_database")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new QuizService(context);
                Quiz q1 = new Quiz { Name = "AAA", Id = 1 };
                Quiz q2 = new Quiz { Name = "BBB", Id = 2 };

                service.CreateQuiz(q1);
                service.CreateQuiz(q2);
            }

            using (var context = new QuizDbContext(options))
            {
                var numberOfQuizesInDb = context.Quiz.Count();
                numberOfQuizesInDb.Should().Be(2);
            }
        }

        [Fact]
        public void Delete_quiz_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_quiz_from_database")
                .Options;

            Add_example_recorods(options);

            using (var context = new QuizDbContext(options))
            {
                var service = new QuizService(context);
                service.DeleteQuizByID(1);

                var numberOfQuizesInDb = context.Quiz.Count();
                var idOfLastedQuiz = context.Quiz.Single().Id;

                numberOfQuizesInDb.Should().Be(1);
                idOfLastedQuiz.Should().Be(2);
            }
        }
        [Fact]
        public void Get_all_quizes_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_all_quizes_from_database")
                .Options;

            Add_example_recorods(options);

            using (var context = new QuizDbContext(options))
            {
                var service = new QuizService(context);
                var actual = service.GetAll();

                var numberOfQuizes = actual.Count();

                numberOfQuizes.Should().Be(2);
            }
        }

        [Fact]
        public void Get_quiz_by_id_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_quiz_id_from_database")
                .Options;

            Add_example_recorods(options);

            using (var context = new QuizDbContext(options))
            {
                var service = new QuizService(context);
                var nameOfQuizWithIdEquals1 = service.GetQuizByID(1).Name;

                nameOfQuizWithIdEquals1.Should().Be("AAA");
            }
        }

        
        [Fact]
        public void Update_quiz()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Update_quiz")
                .Options;

            Add_example_recorods(options);

            using (var context = new QuizDbContext(options))
            {
                var service = new QuizService(context);
                Quiz q1 = new Quiz { Name = "AAB",  Id = 1 };
                service.UpdateQuiz(q1);

                var numberOfQuizesInDb = context.Quiz.Count();
                var nameOfUpdatedQuiz = context.Quiz.FirstOrDefault((x => x.Id == 1)).Name;

                numberOfQuizesInDb.Should().Be(2);
                nameOfUpdatedQuiz.Should().Be("AAB");

            }
        }
    }
}
