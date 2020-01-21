using System;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;
using QuizApp.Models.Services;
using Xunit;
using System.Linq;
using FluentAssertions;

namespace QuizApp.Test
{
    public class QuestionServiceTest
    {
        private static void Add_example_records(DbContextOptions<QuizDbContext> options)
        {
            using (var context = new QuizDbContext(options))
            {
                context.Question.Add(new Question { Content = "AAA", QuizID = 12, Id = 1 });
                context.Question.Add(new Question { Content = "BBB", QuizID = 13, Id = 2 });
                context.SaveChanges();
            }
        }

        [Fact]
        public void Add_question_to_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_question_to_database")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new QuestionService(context);
                Question q1 = new Question { Content = "AAA", QuizID = 12, Id = 1 };
                Question q2 = new Question { Content = "BBB", QuizID = 13, Id = 2 };

                service.CreateQuestion(q1);
                service.CreateQuestion(q2);
            }

            using (var context = new QuizDbContext(options))
            {
                var numberOfQuestionsInDb = context.Question.Count();
                numberOfQuestionsInDb.Should().Be(2);
            }
        }

        [Fact]
        public void Delete_question_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_question_from_database")
                .Options;

            Add_example_records(options);

            using (var context = new QuizDbContext(options))
            {
                var service = new QuestionService(context);
                service.DeleteQuestionByID(1);

                var numberOfQuestionsInDb = context.Question.Count();
                var idOfLastedQuestion = context.Question.Single().Id;

                numberOfQuestionsInDb.Should().Be(1);
                idOfLastedQuestion.Should().Be(2);
            }
        }

        [Fact]
        public void Get_question_by_id_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_question_by_id_from_database")
                .Options;

            Add_example_records(options);
      
            using (var context = new QuizDbContext(options))
            {
                var service = new QuestionService(context);

                var quizIdOfChosenQuestion = service.GetQuestionByID(2).QuizID;

                quizIdOfChosenQuestion.Should().Be(13);
            }
        }

        [Fact]
        public void Get_questions_by_quizID_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_questions_by_quizID_from_database")
                .Options;

            Add_example_records(options);

            using (var context = new QuizDbContext(options))
            {
                var service = new QuestionService(context);
                var actual = service.GetQuestionsByQuizID(12);

                var numberOfQuestionsToSelectedQuiz = actual.Count();

                numberOfQuestionsToSelectedQuiz.Should().Be(1);
            }
        }
        [Fact]
        public void Update_question()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Update_question")
                .Options;

            Add_example_records(options);

            using (var context = new QuizDbContext(options))
            {
                var service = new QuestionService(context);
                Question q1 = new Question { Content = "AAB", QuizID = 1, Id = 1 };
                service.UpdateQuestion(q1);

                var numberOfQuestionsInDb = context.Question.Count();
                var contentOfUpdatedQuestion = context.Question.FirstOrDefault((x => x.Id == 1)).Content;

                numberOfQuestionsInDb.Should().Be(2);
                contentOfUpdatedQuestion.Should().Be("AAB");
            }
        }
    }
}
