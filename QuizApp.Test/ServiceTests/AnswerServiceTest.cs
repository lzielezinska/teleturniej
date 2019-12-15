using System;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;
using QuizApp.Models.Services;
using Xunit;
using System.Linq;


namespace QuizApp.Test.ServiceTests
{
    public class AnswerServiceTest
    {

        [Fact]
        public void Add_answer_to_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_answer_to_database")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new AnswerService(context);
                Answer example_answer = new Answer { Content = "AAA" };
                service.CreateAnswer(example_answer);
                context.SaveChanges();
            }

            using (var context = new QuizDbContext(options))
            {
                Assert.Equal(1, context.Answer.Count());
                Assert.Equal("AAA", context.Answer.Single().Content);
            }
        }

        [Fact]
        public void Delete_answer_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_answer_from_database")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new AnswerService(context);
                Answer a1 = new Answer { Content = "AAA", Id = 1};
                Answer a2 = new Answer { Content = "BBB", Id = 2};

                service.CreateAnswer(a1);
                service.CreateAnswer(a2);

                context.SaveChanges();
            }

            using (var context = new QuizDbContext(options))
            {
                var service = new AnswerService(context);
                service.DeleteAnswerByID(1);
                Assert.Equal(1, context.Answer.Count());
                Assert.Equal("BBB", context.Answer.Single().Content);
                Assert.Equal(2, context.Answer.Single().Id);
            }
        }
        [Fact]
        public void Get_answer_by_id_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_answer_by_id_from_database")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new AnswerService(context);
                Answer a1 = new Answer { Content = "AAA", Id = 1 };
                Answer a2 = new Answer { Content = "BBB", Id = 2 };
                Answer a3 = new Answer { Content = "CCC", Id = 3, QuestionID = 12};

                service.CreateAnswer(a1);
                service.CreateAnswer(a2);
                service.CreateAnswer(a3);

                context.SaveChanges();
            }

            using (var context = new QuizDbContext(options))
            {
                var service = new AnswerService(context);
                var actual = service.GetAnswerByID(3);

                Assert.Equal("CCC", actual.Content);
                Assert.Equal(12, actual.QuestionID);
            }
        }

        [Fact]
        public void Get_answers_by_questionID_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_answers_by_questionID_from_database")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new AnswerService(context);
                Answer a1 = new Answer { Content = "AAA", Id = 1, QuestionID = 1};
                Answer a2 = new Answer { Content = "BBB", Id = 2, QuestionID = 1};
                Answer a3 = new Answer { Content = "CCC", Id = 3, QuestionID = 12};

                service.CreateAnswer(a1);
                service.CreateAnswer(a2);
                service.CreateAnswer(a3);
            }

            using (var context = new QuizDbContext(options))
            {
                var service = new AnswerService(context);
                var actual = service.GetAnswersByQuestionID(1);

                Assert.Equal(2, actual.Count());
                Assert.Equal("AAA", actual[0].Content);
                Assert.Equal("BBB", actual[1].Content);
            }
        }

        [Fact]
        public void Update_answer()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Update_answer")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new AnswerService(context);
                Answer a1 = new Answer { Content = "AAA", Id = 1, QuestionID = 1 };
                Answer a2 = new Answer { Content = "BBB", Id = 2, QuestionID = 1 };

                service.CreateAnswer(a1);
                service.CreateAnswer(a2);

            }

            using (var context = new QuizDbContext(options))
            {
                var service = new AnswerService(context);
                Answer a1 = new Answer { Content = "QWE", Id = 1 };
                service.UpdateAnswer(a1);

                Assert.Equal(2, context.Answer.Count());
                Assert.Equal("QWE", context.Answer.FirstOrDefault((x => x.Id == 1)).Content);
            }
        }

    }
}
