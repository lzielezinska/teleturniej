using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly QuizDbContext _context;

        public QuestionsController(QuizDbContext context)
        {
            _context = context;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            QuestionAnswers model = new QuestionAnswers();

            int questionId = 1;

            var question = await _context.Question
                .FirstOrDefaultAsync(m => m.Id == questionId);
            var answers = _context.Answer
                .Include(x => x.Question)
                .Where(x => x.QuestionID == questionId)
                .ToList();

            model.Question = question;
            model.Answers = answers;

            return View(model);
        }
    }
}
