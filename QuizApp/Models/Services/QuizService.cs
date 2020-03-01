using Microsoft.EntityFrameworkCore;
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

        public IList<Quiz> GetAll()
        {
            return _context.Quiz
                .ToList();
        }

        public Quiz GetQuizByID(int quizId)
        {   
            var quiz = _context.Quiz
                   .Where(q => q.Id == quizId)
                   .Include(q => q.Questions)
                   .ThenInclude(a => a.Answers)
                   .FirstOrDefault();

            return quiz;
        }



        public void UpdateQuiz(Quiz quiz)
        {
            _context.Quiz.Update(quiz);
            _context.SaveChanges();
        }

        public void RemoveDeletedQuestions(Quiz quiz)
        {
            var questions = _context.Question.Where(c => c.QuizID == quiz.Id);
            foreach (var item in questions)
            {
                if(!quiz.Questions.Any(c => c.Id == item.Id))
                    _context.Question.Remove(item);
            }
            _context.SaveChanges();
        }
    }
}
