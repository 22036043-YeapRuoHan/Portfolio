using Microsoft.AspNetCore.Mvc;

namespace Lesson05.Controllers;

public class DemoController : Controller
{
    public IActionResult StyleExercise()
    {
        return View();
    }
    public IActionResult StyleExerciseCompleted()
    {
        return View();
    }

    public IActionResult UseTempData()
    {
        TempData["Data1"] = "Pineapple";
        ViewData["Data2"] = "Orange";
        return RedirectToAction("ShowTempData");
    }

    public IActionResult ShowTempData()
    {
        ViewData["Data3"] = "Banana";
        return View();
    }

    public IActionResult NavBar()
    {
        return View();
    }

    public IActionResult Alerts()
    {
        return View();
    }

    public IActionResult England()
    {
        return View();
    }

    public IActionResult Finland()
    {
        return View("England");
    }

    public IActionResult Holland()
    {
        return RedirectToAction("England");
    }
}
