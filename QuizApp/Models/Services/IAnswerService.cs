using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models.Services
{
    public interface IAnswerService
    {
        Answer GetAnswerByID(int answerId);
        List<Answer> GetAnswersByQuestionID(int questionId);
        void CreateAnswer(Answer answer);
        void UpdateAnswer(Answer answer);
        void DeleteAnswerByID(int answerId);
    }
}
