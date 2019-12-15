using System;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;
using QuizApp.Models.Services;
using Xunit;
using System.Linq;

namespace QuizApp.Test
{
    public class QuestionServiceTest
    {
        [Fact]
        public void Add_question_to_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_question_to_database")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new QuestionService(context);
                Question example_question = new Question { Content = "AAA", QuizID = 1 };
                service.CreateQuestion(example_question);
                context.SaveChanges();
            }

            using (var context = new QuizDbContext(options))
            {
                Assert.Equal(1, context.Question.Count());
                Assert.Equal("AAA", context.Question.Single().Content);
            }
        }

        [Fact]
        public void Delete_question_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_question_from_database")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new QuestionService(context);
                Question q1 = new Question { Content = "AAA", QuizID = 1, Id = 1};
                Question q2 = new Question { Content = "BBB", QuizID = 1, Id = 2};

                service.CreateQuestion(q1);
                service.CreateQuestion(q2);
                context.SaveChanges();
            }

            using (var context = new QuizDbContext(options))
            {
                var service = new QuestionService(context);
                service.DeleteQuestionByID(1);
                Assert.Equal(1, context.Question.Count());
                Assert.Equal("BBB", context.Question.Single().Content);
                Assert.Equal(2, context.Question.Single().Id);
            }
        }

        [Fact]
        public void Get_question_by_id_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_question_by_id_from_database")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new QuestionService(context);
                Question q1 = new Question { Content = "AAA", QuizID = 12, Id = 3 };
                Question q2 = new Question { Content = "BBB", QuizID = 1, Id = 4 };

                service.CreateQuestion(q1);
                service.CreateQuestion(q2);
                context.SaveChanges();
            }

            using (var context = new QuizDbContext(options))
            {
                var service = new QuestionService(context);
                var actual = service.GetQuestionByID(3);

                Assert.Equal("AAA", actual.Content);
                Assert.Equal(12, actual.QuizID);
            }
        }
        [Fact]
        public void Get_questions_by_quizID_from_database()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_questions_by_quizID_from_database")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new QuestionService(context);
                Question q1 = new Question { Content = "AAA", QuizID = 1, Id = 1 };
                Question q2 = new Question { Content = "BBB", QuizID = 1, Id = 2 };
                Question q3 = new Question { Content = "CCC", QuizID = 2, Id = 3 };


                service.CreateQuestion(q1);
                service.CreateQuestion(q2);
                context.SaveChanges();
            }

            using (var context = new QuizDbContext(options))
            {
                var service = new QuestionService(context);
                var actual = service.GetQuestionsByQuizID(1);

                Assert.Equal(2, actual.Count());
                Assert.Equal("AAA", actual[0].Content);
                Assert.Equal("BBB", actual[1].Content);

            }
        }
        [Fact]
        public void Update_question()
        {
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseInMemoryDatabase(databaseName: "Update_question")
                .Options;

            using (var context = new QuizDbContext(options))
            {
                var service = new QuestionService(context);
                Question q1 = new Question { Content = "AAA", QuizID = 1, Id = 1 };

                service.CreateQuestion(q1);
                context.SaveChanges();
            }

            using (var context = new QuizDbContext(options))
            {
                var service = new QuestionService(context);
                Question q1 = new Question { Content = "AAB", QuizID = 1, Id = 1 };
                service.UpdateQuestion(q1);

                Assert.Equal(1, context.Question.Count());
                Assert.Equal("AAB", context.Question.Single().Content);
            }
        }
    }
}
