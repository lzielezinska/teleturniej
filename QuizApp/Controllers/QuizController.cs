﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Data.Repositories;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class QuizController : Controller
    {
        private IRepositoryWrapper _repoWrapper;

        public QuizController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        // GET: Quiz
        public ActionResult Index()
        {
            var model = _repoWrapper.Quiz.GetAll();

            return View(model);
        }

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