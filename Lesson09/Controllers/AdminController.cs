using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lesson09.Controllers;

public class AdminController : Controller
{
    // TODO: Lesson09 Task 6 - Add only the admin role to the [Authorize] attributes (4x)
    [Authorize(Roles = "admin")]
    public IActionResult Users()
    {
        List<TravelUser> list = DBUtl.GetList<TravelUser>("SELECT * FROM TravelUser");
        return View(list);
    }

    [Authorize(Roles = "admin")]
    public IActionResult CreateUser()
    {
        return View();
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult CreateUser(TravelUser usr)
    {
        // TODO: Lesson09 Task 5 - Write secure code to insert TravelUser into database
        if (!ModelState.IsValid)
        {
            ViewData["Message"] = "Invalid Input";
            ViewData["MsgType"] = "warning";
            return View("CreateUser");
        }
        else
        {
            string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;


            string insert = @"INSERT INTO TravelUser(UserId, UserPw, Fullname, 
                                   Email, Dob, UserRole)
                              VALUES('{0}', HASHBYTES('SHA1', '{1}'),'{2}','{3}','{4:yyyy-MM-dd}','{5}')";

            if (DBUtl.ExecSQL(insert, usr.UserId, usr.UserPw, usr.FullName,
                                       usr.Email, usr.Dob,
                                       usr.UserRole) == 1)
            {
                TempData["Message"] = "User Successfully Created.";
                TempData["MsgType"] = "success";
            }
            else
            {
                ViewData["Message"] = DBUtl.DB_Message;
                ViewData["MsgType"] = "danger";
            }
            return RedirectToAction("Users");
        }

    

         //<< Remove this line when your code is complete.
    }

    [Authorize(Roles = "user, admin")]
    public IActionResult Delete(string id)
    {
        string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        if (userid.Equals(id, StringComparison.InvariantCultureIgnoreCase))
        {
            TempData["Message"] = "Own ID cannot be deleted";
            TempData["MsgType"] = "warning";
        }
        else
        {
            string delete = "DELETE FROM TravelUser WHERE UserId='{0}'";
            int res = DBUtl.ExecSQL(delete, id);
            if (res == 1)
            {
                TempData["Message"] = "User Record Deleted";
                TempData["MsgType"] = "success";
            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["MsgType"] = "danger";
            }
        }
        return RedirectToAction("Users");
    }
}

//22036043 Yeap Ruo Han
