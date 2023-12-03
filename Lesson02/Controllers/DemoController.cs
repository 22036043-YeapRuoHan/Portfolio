using Microsoft.AspNetCore.Mvc;

namespace Lesson02.Controllers;

public class DemoController : Controller
{
    public IActionResult SayHello1()
    {
        DoGreeting();
        return View();
    }
    private void DoGreeting()
    {
        DateTime current = DateTime.Now;
        string greet = String.Format("It's {0:HHmm}hrs. ", current);
        if (current.Hour < 12)
            greet += "Good Morning";
        else if (current.Hour < 18)
            greet += "Good Afternoon";
        else
            greet += "Good Evening";
        ViewData["Greeting"] = greet;
    }

    public IActionResult SayHello5()
    {
        DoGreeting();
        return View(); // Views/Demo/SayHello5.cshtml
    }
    public IActionResult SayHello5_Post()
    {
        DoGreeting();
        string name = HttpContext.Request.Form["Name"];
        string salute = HttpContext.Request.Form["Gender"].ToString();
        string membership = HttpContext.Request.Form["Membership"];
        string vegan = HttpContext.Request.Form["Vegan"].ToString();

        ViewData["Message"] = $"Hello {salute} {name} ({membership}), Welcome!";
        if (String.Equals(vegan, "Vegan")) // will work even if vegan == null 
            ViewData["Message"] += " Enjoy your vegan meals.";

        return View("SayHello5"); // Views/Demo/SayHello5.cshtml
    }

    public IActionResult SayHello2()
    {
        DoGreeting();
        return View(); // Views/Demo/SayHello2.cshtml
    }
    public IActionResult SayHello2_Post()
    {
        DoGreeting();
        string name = HttpContext.Request.Form["Name"];
        

        ViewData["Message"] = $"Hello  {name} , Welcome!";
        

        return View("SayHello2"); // Views/Demo/SayHello2.cshtml
    }

    public IActionResult SayHello3()
    {
        DoGreeting();
        return View(); // Views/Demo/SayHello5.cshtml
    }
    public IActionResult SayHello3_Post()
    {
        DoGreeting();
        string name = HttpContext.Request.Form["Name"];
        string salute = HttpContext.Request.Form["Gender"].ToString();
        

        ViewData["Message"] = $"Hello  {salute} {name} , Welcome!";
        

        return View("SayHello3"); // Views/Demo/SayHello3.cshtml
    }

    public IActionResult SayHello4()
    {
        DoGreeting();
        return View(); // Views/Demo/SayHello4.cshtml
    }
    public IActionResult SayHello4_Post()
    {
        DoGreeting();
        string name = HttpContext.Request.Form["Name"];
        string salute = HttpContext.Request.Form["Gender"].ToString();
        string membership = HttpContext.Request.Form["Membership"];
        

        ViewData["Message"] = $"Hello {salute} {name} ({membership}), Welcome!";
       
        return View("SayHello4"); // Views/Demo/SayHello4.cshtml
    }


}
//22036043 Yeap Ruo Han
