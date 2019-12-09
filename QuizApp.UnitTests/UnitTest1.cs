using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using QuizApp.Data;
using QuizApp.Data.Repositories;
using QuizApp.Models;
using Xunit;

namespace QuizApp.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var answers_example = new List<Answer>
            {
                new Answer { Content = "BBB", QuestionID = 1, IsCorrect = true},
                new Answer { Content = "ZZZ", QuestionID = 1},
                new Answer { Content = "AAA", QuestionID = 1},
                new Answer { Content = "KKK", QuestionID = 1},
            };

            var data = new List<Question>
            {
                new Question { Id = 1, Content = "BBB",  Answers = answers_example},
            };

            var mockContext = new Mock<QuizDbContext>();
            var mocked_question = CreateDbSetMock(data);

            mockContext.Setup(c => c.Question).Returns(mocked_question.Object);

            var repositoryWrapper = new RepositoryWrapper(mockContext.Object);
            var answers = repositoryWrapper.Answer.GetByCondition(x => x.QuestionID == 1).ToList();

            Assert.Equal("AAA", answers.ElementAt(0).Content);
        }
        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }

      
    }
}

