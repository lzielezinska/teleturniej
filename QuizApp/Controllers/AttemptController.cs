using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Models.Services;

namespace QuizApp.Controllers
{
    public class AttemptController : Controller
    {
        private IAttemptService _attemptService;
        private UserManager<IdentityUser> _userManager;
        public AttemptController(IAttemptService attemptService,
            UserManager<IdentityUser> userManager)
        {
            _attemptService = attemptService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var user = _userManager.GetUserId(User);
            var model = _attemptService.GetAll(user);

            return View(model);
        }
    }
}