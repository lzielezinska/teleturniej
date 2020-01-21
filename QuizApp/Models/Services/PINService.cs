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
    }
}
