using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;
using QuizApp.Models.Services;
using Xunit;
using System.Linq;
using FluentAssertions;



namespace QuizApp.Test.ServiceTests
{
    public class AnswerServiceTest
    {
        private static void Add_example_records(DbContextOptions<QuizDbContext> options)
        {
            using (var context = new QuizDbContext(options))
            {
                context.Answer.Add(new Answer { Content = "AAA", Id = 1, QuestionID = 12 });
                context.Answer.Add(new Answer { Content = "BBB", Id = 2, QuestionID = 13 });
                context.SaveChanges();
            }
        }

        [Fact]
        public void Add_answer_to_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_answer_to_database")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new AnswerService(context);
                Answer a1 = new Answer { Content = "AAA", Id = 1, QuestionID = 12 };
                Answer a2 = new Answer { Content = "BBB", Id = 2, QuestionID = 13 };

                service.CreateAnswer(a1);
                service.CreateAnswer(a2);
            }

            using (var context = new QuizDbContext(options))
            {
                var numberOfAnserwsInDb = context.Answer.Count();
                numberOfAnserwsInDb.Should().Be(2);
            }
        }

        [Fact]
        public void Delete_answer_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_answer_from_database")
                .Options;

            Add_example_records(options);

            using (var context = new QuizDbContext(options))
            {
                var service = new AnswerService(context);
                service.DeleteAnswerByID(1);

                var numberOfAnserwsInDb = context.Answer.Count();
                var idOfLastedAnswer = context.Answer.Single().Id;

                numberOfAnserwsInDb.Should().Be(1);
                idOfLastedAnswer.Should().Be(2);
            }
        }
        [Fact]
        public void Get_answer_by_id_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_answer_by_id_from_database")
                .Options;

            Add_example_records(options);

            using (var context = new QuizDbContext(options))
            {
                var service = new AnswerService(context);
                var questionIdOfChosenAnswer = service.GetAnswerByID(2).QuestionID;

                questionIdOfChosenAnswer.Should().Be(13);
            }
        }

        [Fact]
        public void Get_answers_by_questionID_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_answers_by_questionID_from_database")
                .Options;

            Add_example_records(options);

            using (var context = new QuizDbContext(options))
            {
                var service = new AnswerService(context);
                var actual = service.GetAnswersByQuestionID(12);

                var numberOfAnswersToSelectedQuestion = actual.Count();

                numberOfAnswersToSelectedQuestion.Should().Be(1);
            }
        }

        [Fact]
        public void Update_answer()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Update_answer")
                .Options;

            Add_example_records(options);

            using (var context = new QuizDbContext(options))
            {
                var service = new AnswerService(context);
                Answer a1 = new Answer { Content = "QWE", Id = 1 };
                service.UpdateAnswer(a1);

                var numberOfAnserwsInDb = context.Answer.Count();
                var contentOfUpdatedAnswer = context.Answer.FirstOrDefault((x => x.Id == 1)).Content;

                numberOfAnserwsInDb.Should().Be(2);
                contentOfUpdatedAnswer.Should().Be("QWE");
            }
        }

    }
}
