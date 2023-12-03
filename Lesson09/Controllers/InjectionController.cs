using Microsoft.AspNetCore.Mvc;

namespace Lesson09.Controllers;

public class InjectionController : Controller
{
    public IActionResult Demo()
    {
        List<Fruit> list = DBUtl.GetList<Fruit>("SELECT * FROM Fruit");
        return View(list);
    }

    [HttpPost]
    public IActionResult Insecure()
    {
        IFormCollection form = HttpContext.Request.Form;
        string fruitname = form["FruitName"].ToString().Trim();

        string sql = @"INSERT INTO Fruit(name) VALUES('{0}')";
        string insert = string.Format(sql, fruitname);
        DBUtl.ExecSQL(insert);

        // Return all fruits to the view.
        List<Fruit> list = DBUtl.GetList<Fruit>("SELECT * FROM Fruit");
        return View("Demo", list);
    }

    [HttpPost]
    public IActionResult Secure()
    {
        IFormCollection form = HttpContext.Request.Form;
        string fruitname = form["FruitName"].ToString().Trim();

        string insert = "INSERT INTO Fruit(name) VALUES('{0}')";
        DBUtl.ExecSQL(insert, fruitname);

        // Return all fruits to the view.
        List<Fruit> list = DBUtl.GetList<Fruit>("SELECT * FROM Fruit");
        return View("Demo", list);
    }

    [HttpPost]
    public IActionResult Secure2()
    {
        IFormCollection form = HttpContext.Request.Form;
        string fruitname = form["FruitName"].ToString().Trim();

        string sql = "INSERT INTO Fruit(name) VALUES('{0}')";
        string insert = String.Format(sql, fruitname.EscQuote());
        DBUtl.ExecSQL(insert);

        // Return all fruits to the view.
        List<Fruit> list = DBUtl.GetList<Fruit>("SELECT * FROM Fruit");
        return View("Demo", list);
    }

}
