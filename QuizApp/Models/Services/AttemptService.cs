using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models.Services
{
    public class AttemptService : IAttemptService
    {
        private QuizDbContext _context;
        public AttemptService(QuizDbContext context)
        {
            _context = context;
        }
        public void CreateAttempt(Attempt attempt)
        {
            _context.Attempt.Add(attempt);
            _context.SaveChanges();
        }

        public IList<AttemptViewModel> GetAll(string userId)
        {
            var attempts = _context.Attempt.Where(x => x.UserId == userId)
                .Include(e => e.Results)
                .Include(e => e.PIN.Quiz);
            var model = new List<AttemptViewModel>();
            foreach (var attemp in attempts)
            {
                int correct = 0;
                int total = 0;
                foreach (var result in attemp.Results)
                {
                    if (result.Response)
                    {
                        correct++;
                    }
                    total++;
                }
                model.Add(new AttemptViewModel { attempt = attemp, total = total, correct = correct });
            }
            return model;
        }
    }
}
