using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models.Services
{
    public interface IQuestionService
    {
        Question GetQuestionByID(int questionId);
        List<Question> GetQuestionsByQuizID(int quizId);
        void CreateQuestion(Question question);
        void UpdateQuestion(Question question);
        void DeleteQuestionByID(int questionId);
    }
}
