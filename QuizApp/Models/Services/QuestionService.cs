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
