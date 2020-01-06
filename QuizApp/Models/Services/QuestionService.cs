using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models.Services
{
    public class QuestionService : IQuestionService
    {
        private QuizDbContext _context;
        public QuestionService(QuizDbContext context)
        {
            _context = context;
        }

        public void CreateQuestion(Question question)
        {
            _context.Question.Add(question);
            _context.SaveChanges();
        }

        public void DeleteQuestionByID(int questionId)
        {
            var question = _context.Question.FirstOrDefault(x => x.Id == questionId);
            if (question != null)
            {
                _context.Question.Remove(question);
                _context.SaveChanges();
            }
        }

        public QuestionAnswersViewModel GetCurrentQuestion(int quizId, int questionIndex)
        {
            var questions = _context.Question.Where(x => x.QuizID == quizId).OrderBy(x => x.Id);
            var lastQuestionId = questions.LastOrDefault()?.Id;
            // ElementAt() is not supported for entities, had to do Skip().First()
            // https://docs.microsoft.com/pl-pl/dotnet/framework/data/adonet/ef/language-reference/supported-and-unsupported-linq-methods-linq-to-entities
            var currentQuestion = questions.Include(e => e.Answers).Skip(questionIndex).FirstOrDefault();

            return new QuestionAnswersViewModel
            {
                Question = currentQuestion,
                IsAnswerFinal = lastQuestionId == currentQuestion.Id
            };
        }

        public Question GetQuestionByID(int questionId)
        {
            return _context.Question
                .FirstOrDefault(x => x.Id == questionId);
        }

        public IList<Question> GetQuestionsByQuizID(int quizId)
        {
            return _context.Question
                .Where(x => x.QuizID == quizId)
                .ToList();
        }

        public void UpdateQuestion(Question question)
        {
            _context.Question.Update(question);
            _context.SaveChanges();
        }
    }
}
