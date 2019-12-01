using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Data.Repositories
{
    public interface IRepositoryWrapper
    {
        IQuizRepository Quiz { get; }
        IQuestionRepository Question { get; }
        IAnswerRepository Answer { get; }
        void Save();
    }
}
