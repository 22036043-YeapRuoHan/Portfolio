using Lesson10.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lesson10.Controllers;

public class DemoController : Controller
{
    public IActionResult SendEmail()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SendEmail(Email email)
    {
        string template = "Dear {0} \n\r" +
                          "<p>{1}</p> \n\r" +
                          "<p>Your redemption code for free gift is <b>{2}</b>.</p> \n\r" +
                          "Marketing Manager.";
        string giftcode = Guid.NewGuid().ToString()[..12];
        string body = String.Format(template, email.CustomerName, email.Message, giftcode);

        if (EmailUtl.SendEmail(email.CustomerEmail, email.Subject, body, out string result))
        {
            ViewData["Message"] = "Email Successfully Sent";
            ViewData["MsgType"] = "success";
        }
        else
        {
            ViewData["Message"] = result;
            ViewData["MsgType"] = "warning";
        }
        return View();
    }

    public IActionResult ServerValidation()
    {
        return View();
    }

    public IActionResult CheckBalance(double amount)
    {
        double balance =
           Double.Parse(DBUtl.GetTable("SELECT 99.9").Rows[0][0].ToString()!);
        if (amount > balance)
        {
            return Json($"Not enough balance for withdrawal");
        }
        return Json(true);
    }

    public IActionResult CheckOrder1(int first, int second, int third)
    {
        // Test of first against second and third
        if (first > second || first > third)
            return Json($"First must be before second and third");
        return Json(true);
    }

    public IActionResult CheckOrder2(int second, int first, int third)
    {
        // Test of second against first and third
        if (second < first || second > third)
            return Json($"Second must be between first and third");
        return Json(true);
    }

    public IActionResult CheckOrder3(int third, int first, int second)
    {
        // Test of third against first and second
        if (third < first || third < second)
            return Json($"Third must be after first and second");
        return Json(true);
    }

    public IActionResult CheckDates(DateTime begin, DateTime end)
    {
        if (begin > end)
        {
            return Json($"Start Date must be earlier than End Date");
        }
        return Json(true);
    }
}
