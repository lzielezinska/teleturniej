using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models.Services
{
    public interface IAttemptService
    {
        void CreateAttempt(Attempt attempt);
        IList<AttemptViewModel> GetAll(string userId);
    }
}
