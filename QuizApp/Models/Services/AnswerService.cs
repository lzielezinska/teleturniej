using QuizApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models.Services
{
    public class AnswerService : IAnswerService
    {
        private QuizDbContext _context;
        public AnswerService(QuizDbContext context)
        {
            _context = context;
        }

        public void CreateAnswer(Answer answer)
        {
            _context.Answer.Add(answer);
            _context.SaveChanges();
        }

        public void DeleteAnswerByID(int answerId)
        {
            var answer = _context.Answer.FirstOrDefault(x => x.Id == answerId);
            if (answer != null)
            {
                _context.Answer.Remove(answer);
                _context.SaveChanges();
            }
        }

        public Answer GetAnswerByID(int answerId)
        {
            return _context.Answer
                .FirstOrDefault(x => x.Id == answerId);
        }

        public IList<Answer> GetAnswersByQuestionID(int questionId)
        {
            return _context.Answer
                .Where(x => x.QuestionID == questionId)
                .ToList();
        }

        public void UpdateAnswer(Answer answer)
        {
            _context.Answer.Update(answer);
            _context.SaveChanges();
        }
    }
}
