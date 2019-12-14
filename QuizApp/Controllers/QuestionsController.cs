using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Data.Repositories;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class QuestionsController : Controller
    {
        private IRepositoryWrapper _repoWrapper;

        public QuestionsController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        // GET: Questions
        [Authorize(Roles = "User")]
        public IActionResult Index(int? id)
        {
            QuestionAnswersViewModel model = new QuestionAnswersViewModel();

            var question = _repoWrapper.Question.GetByCondition(m => m.Id == id).FirstOrDefault();
            var answers = _repoWrapper.Answer.GetByCondition(x => x.QuestionID == id).ToList();

            model.Question = question;
            model.Answers = answers;

            return View(model);
        }
    }
}
