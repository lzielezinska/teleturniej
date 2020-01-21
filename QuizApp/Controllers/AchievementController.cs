using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using QuizApp.Models.Services;

namespace QuizApp.Controllers
{
    public class AchievementController : Controller
    {
        private IPINService _pinService;
        private UserManager<IdentityUser> _userManager;
        public AchievementController(IPINService pinService,
            UserManager<IdentityUser> userManager)
        {
            _pinService = pinService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Lecturer")]
        public IActionResult Index()
        {
            var user = _userManager.GetUserId(User);
            List<PIN> pins = _pinService.FindForUser(user);
            return View(pins);
        }

        [Authorize(Roles = "Lecturer")]
        public IActionResult ShowResults(int id)
        {
            List<AttemptViewModel> model = _pinService.FindResults(id);

            return View(model);
        }
    }
}