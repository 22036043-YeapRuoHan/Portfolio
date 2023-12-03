using Lesson02.Models;
using Microsoft.AspNetCore.Mvc;
namespace Lesson02.Controllers
{
  public class HomeController : Controller
  {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            // Create the model to for the View
            Greeting greet = new("SOI Students",
                                 "C236 Module Chair",
                                 "Welcome to Web App Dev in .NET",
                                @"This is a useful module for your FYP.
                              Study hard and All the BEST!");

            // Put the model in ViewData 
            ViewData["Hello"] = greet;

            // Use the default Welcome.cshtml view in Views/Home folder
            return View();
        }

    }
}

