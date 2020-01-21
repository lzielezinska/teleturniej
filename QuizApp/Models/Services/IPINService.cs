using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models.Services
{
    public interface IPINService
    {
        void CreatePIN(PIN pin);
        void DisablePIN(string code);
        GeneratePINViewModel GeneratePIN(int quizId, string userId);
        PIN FindByCode(string code);
        List<PIN> FindForUser(string userId);
        List<AttemptViewModel> FindResults(int id);
    }
}
