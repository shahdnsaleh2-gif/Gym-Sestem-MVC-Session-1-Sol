using System.Diagnostics;
using Gym_Sestem_MVC_Session_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Sestem_MVC_Session_1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
