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

        [Authorize(Roles = "User")]
        public IActionResult Question(int quizId, int numberOfQuestion)
        {
            QuestionAnswersViewModel model = new QuestionAnswersViewModel();

            var quizQuestions = _questionService.GetQuestionsByQuizID(quizId);
            var question = quizQuestions.ElementAt(numberOfQuestion - 1);
            var answers = _answerService.GetAnswersByQuestionID(question.Id);

            model.Question = question;
            model.Answers = answers;
            model.NextQuestionNumber = numberOfQuestion + 1;

            return View(model);
        }
    }
}