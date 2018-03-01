using System;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactChatDemo.Models;
using ReactChatDemo.User;

namespace ReactChatDemo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserTracker _userTracker;

        public HomeController(IUserTracker userTracker)
        {
            _userTracker = userTracker;
        }
        public IActionResult Index()
        {
            var user = HttpContext.User?.Identity as ClaimsIdentity;
            if (user != null && user.IsAuthenticated)
            {
                _userTracker.AddUser(user.FindFirst("sid").Value, user.Name);
            }

            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
