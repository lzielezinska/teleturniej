﻿using System;
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
        private IQuestionService _questionService;
        private IAnswerService _answerService;

        public QuizController(IQuizService quizService, 
            IQuestionService questionService, 
            IAnswerService answerService)
        {
            _quizService = quizService;
            _questionService = questionService;
            _answerService = answerService;
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
        public IActionResult Question(int quizId, int numberOfQuestion)
        {
            var question = _questionService.GetCurrentQuestion(quizId, numberOfQuestion);

            ViewBag.ID = numberOfQuestion;

            return View(question);
        }
    }
}