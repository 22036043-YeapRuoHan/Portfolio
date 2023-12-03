using Microsoft.AspNetCore.Mvc;

namespace Lesson09.Controllers;

public class ValidationController : Controller
{
    public IActionResult Demo()
    {
        DemoData dd = new()
           {
               DateFieldA = DateTime.Today,
               DateFieldB = DateTime.Now,
           };
        return View(dd);
    }

    [HttpPost]
    public IActionResult Demo(DemoData _)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Message"] = "ModelState is NOT valid";
            ViewData["MsgType"] = "warning";
        }
        else
        {
            ViewData["Message"] = "ModelState is VALID";
            ViewData["MsgType"] = "info";
        }
        return View();
    }
}
