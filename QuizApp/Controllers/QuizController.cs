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
        private UserManager<IdentityUser> _userManager;

        public QuizController(IQuizService quizService,
            IQuestionService questionService,
            IAnswerService answerService,
            IAttemptService attemptService,
            IResultService resultService,
            UserManager<IdentityUser> userManager)
        {
            _quizService = quizService;
            _questionService = questionService;
            _answerService = answerService;
            _attemptService = attemptService;
            _resultService = resultService;
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
        public IActionResult GeneratePIN(int id)
        {
            GeneratePINViewModel model = _quizService.GeneratePIN(id);

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
        [Route("Quiz/{quizId}/Question/{numberOfQuestion}", Name = "QuestionView")]
        public IActionResult Question(int quizId, int numberOfQuestion)
        {
            if (numberOfQuestion == 0)
            {
                var user =  _userManager.GetUserId(User);
                var attempt = new Attempt{ QuizID = quizId, UserId = user, StartDate = DateTime.Now };
                _attemptService.CreateAttempt(attempt);
                HttpContext.Session.SetInt32("attempt", attempt.Id);
            }
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
            var result = new Result { QuestionID = value.question, AttemptID = value.attempt, Response = value.correct};
            _resultService.CreateResult(result);
            return "OK";
        }
    }
}

public class ResultRequest
{
    public int attempt { get; set; }
    public bool correct { get; set; }
    public int question { get; set; }
 
}