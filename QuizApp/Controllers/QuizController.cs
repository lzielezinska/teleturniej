using System;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using QuizApp.Models.Services;
using Microsoft.AspNetCore.Http;

namespace QuizApp.Controllers
{
    public class QuizController : Controller
    {
        private IQuizService _quizService;
        private IQuestionService _questionService;
        private IAnswerService _answerService;
        private IAttemptService _attemptService;
        private IResultService _resultService;
        private IPINService _pinService;
        private UserManager<IdentityUser> _userManager;

        public QuizController(IQuizService quizService,
            IQuestionService questionService,
            IAnswerService answerService,
            IAttemptService attemptService,
            IResultService resultService,
            IPINService pinService,
            UserManager<IdentityUser> userManager)
        {
            _quizService = quizService;
            _questionService = questionService;
            _answerService = answerService;
            _attemptService = attemptService;
            _resultService = resultService;
            _pinService = pinService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Lecturer")]
        // GET: Quiz
        public IActionResult Index()
        {
            var model = _quizService.GetAll();

            return View(model);
        }

        [Authorize(Roles = "Lecturer")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        public IActionResult Create([Bind("Name,Questions")]Quiz quiz)
        {
            _quizService.CreateQuiz(quiz);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Lecturer")]
        public IActionResult Edit(int id)
        {
            var quiz = _quizService.GetQuizByID(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return View(quiz);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Lecturer")]
        public IActionResult Edit([Bind("Id,Name,Questions")]Quiz quiz)
        {
            _quizService.UpdateQuiz(quiz);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Lecturer")]
        public IActionResult GeneratePIN(int id)
        {
            var user = _userManager.GetUserId(User);
            GeneratePINViewModel model = _pinService.GeneratePIN(id, user);

            return View(model);
        }

        [Authorize(Roles = "User")]
        public IActionResult Result()
        {
            var attempId = HttpContext.Session.GetInt32("attempt");
            ResultViewModel model = _resultService.GetResult(attempId);
            return View(model); 
        }

        [Authorize(Roles = "User")]
        public IActionResult EnterPIN()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        [Route("Quiz/{quizId}/Question/{numberOfQuestion}", Name = "QuestionView")]
        public IActionResult Question(int quizId, int numberOfQuestion)
        {
            var question = _questionService.GetCurrentQuestion(quizId, numberOfQuestion);
            question.AttemptId = HttpContext.Session.GetInt32("attempt");

            ViewBag.ID = numberOfQuestion;

            return View(question);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("Quiz/{quizId}/Result")]
        public string AddResult(int quizId, [FromBody] ResultRequest value)
        {
            _resultService.UpdateResult(value.attempt, value.question, value.correct);
            return "OK";
        }

        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        [Route("Quiz/DisablePIN")]
        public string DisablePIN([FromBody] PINRequest value)
        {
            _pinService.DisablePIN(value.code);
            return "OK";
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("Quiz/Start")]
        public int Start([FromBody] PINRequest value)
        {
            PIN pin = _pinService.FindByCode(value.code);
            if (pin != null)
            {
                var user = _userManager.GetUserId(User);
                var attempt = new Attempt { PINID = pin.Id, UserId = user, StartDate = DateTime.Now };
                _attemptService.CreateAttempt(attempt);
                HttpContext.Session.SetInt32("attempt", attempt.Id);
                var questions = _questionService.GetQuestionsByQuizID(pin.QuizID);
                foreach (var question in questions)
                {
                    var result = new Result { QuestionID = question.Id, AttemptID = attempt.Id, Response = false };
                    _resultService.CreateResult(result);
                }
                return pin.QuizID;
            }
            return 0;
        }
    }
}

public class ResultRequest
{
    public int attempt { get; set; }
    public bool correct { get; set; }
    public int question { get; set; }
 
}
public class PINRequest
{
    public string code { get; set; }
}