using Lesson10.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lesson10.Controllers;

public class AccountController : Controller
{
    private const string LOGIN_SQL =
       @"SELECT * FROM LemonUser 
            WHERE UserId = '{0}' 
              AND UserPw = HASHBYTES('SHA1', '{1}')";

    private const string LASTLOGIN_SQL =
       @"UPDATE LemonUser SET LastLogin=GETDATE() WHERE UserId='{0}'";

    private const string ROLE_COL = "UserRole";
    private const string NAME_COL = "FullName";

    private const string REDIRECT_CNTR = "Performance";
    private const string REDIRECT_ACTN = "Index";

    private const string LOGIN_VIEW = "UserLogin";

    [AllowAnonymous]
    public IActionResult Login(string returnUrl = null!)
    {
        TempData["ReturnUrl"] = returnUrl;
        return View(LOGIN_VIEW);
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login(UserLogin user)
    {
        if (!AuthenticateUser(user.UserID, user.Password, out ClaimsPrincipal principal))
        {
            ViewData["Message"] = "Incorrect User ID or Password";
            ViewData["MsgType"] = "warning";
            return View(LOGIN_VIEW);
        }
        else
        {
            HttpContext.SignInAsync(
               CookieAuthenticationDefaults.AuthenticationScheme,
               principal,
               new AuthenticationProperties
               {
                   ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
               });

            // Update the Last Login Timestamp of the User
            DBUtl.ExecSQL(LASTLOGIN_SQL, user.UserID);

            if (TempData["returnUrl"] != null)
            {
                string returnUrl = TempData["returnUrl"]!.ToString()!;
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
            }

            return RedirectToAction(REDIRECT_ACTN, REDIRECT_CNTR);
        }
    }

    [Authorize]
    public IActionResult Logoff(string returnUrl = null!)
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);
        return RedirectToAction(REDIRECT_ACTN, REDIRECT_CNTR);
    }

    [AllowAnonymous]
    public IActionResult Forbidden()
    {
        return View();
    }

    [Authorize(Roles = "manager")]
    public IActionResult Users()
    {
        List<LemonUser> list = DBUtl.GetList<LemonUser>("SELECT * FROM LemonUser WHERE UserRole='member'");
        return View(list);
    }

    [Authorize(Roles = "manager")]
    public IActionResult Delete(string id)
    {
        string delete = "DELETE FROM LemonUser WHERE UserId='{0}'";
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

        return RedirectToAction("Users");
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        return View("UserRegister");
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Register(LemonUser usr)
    {
        ModelState.Remove("UserRole");     // All new users have role set to 'member'.
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                ViewData["Message"] += string.Format(error.ErrorMessage);
            }
            //ViewData["Message"] = "Invalid Input";
            ViewData["MsgType"] = "danger";
            return View("UserRegister");
        }
        else
        {
            // TODO: Lesson10 Task 2a - Provide the SQL statement to register new member"
            string insert = @"INSERT INTO LemonUser(UserID,UserPw,FullName, Email, UserRole) VALUES
                   ('{0}', HASHBYTES('SHA1', '{1}'), '{2}', '{3}', 'member')";
                // TODO: Lesson10 Task 2b - Remove this line: For initial compilation only.
              // TODO: Lesson10 Task 2c - Remove this line: For initial compilation only.

            if (DBUtl.ExecSQL(insert, usr.UserId, usr.UserPw, usr.FullName, usr.Email) == 1)
            {
                string template = "Hi {0}, \n\r" +
                                  "Welcome to Lemonade Chamber Music! \n\r" +
                                  "Your userid is <b>{1}</b> and your password is <b>{2}</b>. \n\r" +
                                  "Manager";
                //TODO: Lesson10 Task 2d - Uncomment the following line
                string title = "Registration Successful - Welcome"; 
                string message = String.Format(template, usr.FullName, usr.UserId, usr.UserPw);

                // TODO: Lesson10 Task 2e - Call EmailUtl.SendEmail to send email
                string result;
                bool outcome = EmailUtl.SendEmail(usr.Email, title, message, out result);

                result = "Something went wrong.";
                if (outcome)
                {
                    ViewData["Message"] = "User Successfully Registered";
                    ViewData["MsgType"] = "success";
                }
                else
                {
                    ViewData["Message"] = result;
                    ViewData["MsgType"] = "warning";
                }
            }
            else
            {
                ViewData["Message"] = DBUtl.DB_Message;
                ViewData["MsgType"] = "danger";
            }
            return View("UserRegister");
        }
    }

    [AllowAnonymous]
    public IActionResult VerifyUserID(string userId)
    {
        string select = $"SELECT * FROM LemonUser WHERE Userid='{userId}'";
        if (DBUtl.GetTable(select).Rows.Count > 0)
        {
            return Json($"[{userId}] already in use");
        }
        return Json(true);
    }

    private static bool AuthenticateUser(string uid, string pw, out ClaimsPrincipal principal)
    {
        principal = null!;

        DataTable ds = DBUtl.GetTable(LOGIN_SQL, uid, pw);
        if (ds.Rows.Count == 1)
        {
            principal =
               new ClaimsPrincipal(
                  new ClaimsIdentity(
                     new Claim[] {
                     new Claim(ClaimTypes.NameIdentifier, uid),
                     new Claim(ClaimTypes.Name, ds.Rows[0][NAME_COL].ToString()!),
                     new Claim(ClaimTypes.Role, ds.Rows[0][ROLE_COL].ToString()!)
                     }, "Basic"
                  )
               );
            return true;
        }
        return false;
    }

}

//22036043 Yeap Ruo Han
