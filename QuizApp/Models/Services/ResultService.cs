using QuizApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models.Services
{
    public class ResultService : IResultService
    {
        private QuizDbContext _context;
        public ResultService(QuizDbContext context)
        {
            _context = context;
        }
        public void CreateResult(Result result)
        {
            _context.Result.Add(result);
            _context.SaveChanges();
        }

        public ResultViewModel GetResult(int? attemp)
        {
            int correct = 0;
            int total = 0;
            var results = _context.Result.Where(x => x.AttemptID == attemp);
            foreach (var result in results)
            {
                if (result.Response)
                {
                    correct++;
                }
                total++;
            }
            return new ResultViewModel { correct = correct, total = total };
        }

        public void UpdateResult(int attemp, int question, bool value)
        {
            var result = _context.Result.Where(x => x.AttemptID == attemp)
                .Where(x => x.QuestionID == question)
                .FirstOrDefault();
            result.Response = value;
            _context.Result.Update(result);
            _context.SaveChanges();
        }
    }
}
