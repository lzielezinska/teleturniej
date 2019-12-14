using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using QuizApp.Models.Services;

namespace QuizApp.Controllers
{
    public class QuizController : Controller
    {
        private IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [Authorize(Roles = "Lecturer")]
        // GET: Quiz
        public ActionResult Index()
        {
            var model = _quizService.GetAll();

            return View(model);
        }

        [Authorize(Roles = "Lecturer")]
        public ActionResult GeneratePIN(int id)
        {
            Random rnd = new Random();
            GeneratePINViewModel model = new GeneratePINViewModel();

            int[] pin = new int[6];
            for (int i = 0; i < 6; i++)
            {
                pin[i] = rnd.Next(0, 10);
            }

            string resultPin = string.Join("", pin);
            model.pin = resultPin;

            return View(model);
        }
    }
}