using QuizApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models.Services
{
    public class QuizService : IQuizService
    {
        private QuizDbContext _context;
        public QuizService(QuizDbContext context)
        {
            _context = context;
        }

        public void CreateQuiz(Quiz quiz)
        {
            _context.Quiz.Add(quiz);
            _context.SaveChanges();
        }

        public void DeleteQuizByID(int quizId)
        {
            var answer = _context.Quiz.FirstOrDefault(x => x.Id == quizId);
            if (answer != null)
            {
                _context.Quiz.Remove(answer);
                _context.SaveChanges();
            }
        }

        public GeneratePINViewModel GeneratePIN(int quizId)
        {
            Random rnd = new Random();
            string pin = rnd.Next(100000, 999999).ToString();

            return new GeneratePINViewModel
            {
                PIN = pin
            };
        }

        public IList<Quiz> GetAll()
        {
            return _context.Quiz
                .ToList();
        }

        public Quiz GetQuizByID(int quizId)
        {
            return _context.Quiz
                .FirstOrDefault(x => x.Id == quizId);
        }

        public void UpdateQuiz(Quiz quiz)
        {
            _context.Quiz.Update(quiz);
            _context.SaveChanges();
        }
    }
}
