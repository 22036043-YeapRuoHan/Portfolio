using Microsoft.AspNetCore.Mvc;

namespace Lesson13.Controllers;

public class DefaultController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
