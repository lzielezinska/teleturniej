using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models.Services
{
    public interface IQuizService
    {
        Quiz GetQuizByID(int quizId);
        IList<Quiz> GetAll();
        void CreateQuiz(Quiz quiz);
        void UpdateQuiz(Quiz quiz);
        void DeleteQuizByID(int quizId);
    }
}
