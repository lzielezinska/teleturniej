using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models.Services
{
    public class PINService : IPINService
    {
        private QuizDbContext _context;
        public PINService(QuizDbContext context)
        {
            _context = context;
        }
        public void CreatePIN(PIN pin)
        {
            _context.PIN.Add(pin);
            _context.SaveChanges();
        }
        public void DisablePIN(string code)
        {
            PIN pin = _context.PIN.FirstOrDefault(x => x.Code == code);
            pin.Active = false;
            _context.PIN.Update(pin);
            _context.SaveChanges();
        }
        public GeneratePINViewModel GeneratePIN(int quizId, string userId)
        {
            Random rnd = new Random();
            string code = rnd.Next(100000, 999999).ToString();
            PIN pin = new PIN { Active = true, Code = code, QuizID = quizId, UserId = userId };
            this.CreatePIN(pin);

            return new GeneratePINViewModel
            {
                PIN = code,
            };
        }
        public PIN FindByCode(string code)
        {
            PIN pin = _context.PIN.Where(x => x.Code == code).Where(x => x.Active == true).FirstOrDefault();
            return pin;
        }
        
        public List<PIN> FindForUser(string userId)
        {
            List<PIN> pins = _context.PIN.Where(x => x.UserId == userId).Where(x => x.Active == false)
                .Include(e => e.Quiz).ToList();
            return pins;
        }

        public List<AttemptViewModel> FindResults(int id)
        {
            PIN pin = _context.PIN.Where(x => x.Id == id).Include(e => e.Attempts).FirstOrDefault();
            var model = new List<AttemptViewModel>();
            foreach (var attempt in pin.Attempts)
            {
                var attemptUser = _context.Attempt.Where(x => x.Id == attempt.Id).Include(e => e.User).FirstOrDefault();
                attempt.User = attemptUser.User;
                List<Result> results = _context.Result.Where(x => x.AttemptID == attempt.Id).ToList();
                int correct = 0;
                int total = 0;
                foreach (var result in results)
                {
                    if (result.Response)
                    {
                        correct++;
                    }
                    total++;
                }
                model.Add(new AttemptViewModel { attempt = attempt, total = total, correct = correct });
            }
            return model;
        }
    }
}
