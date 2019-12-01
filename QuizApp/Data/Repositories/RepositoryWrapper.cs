using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Data.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private QuizDbContext _context;
        private IQuizRepository _quiz;
        private IQuestionRepository _question;
        private IAnswerRepository _answer;

        public IQuizRepository Quiz
        {
            get
            {
                if(_quiz == null)
                {
                    _quiz = new QuizRepository(_context);
                }

                return _quiz;
            }
        }

        public IQuestionRepository Question
        {
            get
            {
                if (_question == null)
                {
                    _question = new QuestionRepository(_context);
                }

                return _question;
            }
        }

        public IAnswerRepository Answer
        {
            get
            {
                if (_answer == null)
                {
                    _answer = new AnswerRepository(_context);
                }

                return _answer;
            }
        }

        public RepositoryWrapper(QuizDbContext quizDbContext)
        {
            _context = quizDbContext;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
