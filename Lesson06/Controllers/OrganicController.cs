using Microsoft.AspNetCore.Mvc;

namespace Lesson06.Controllers;

public class OrganicController : Controller
{
    public IActionResult Index()
    {
        return View("Organic");
    }

    public IActionResult Products()
    {
        string sql = "SELECT * FROM OrgProduct";
        DataTable dt = DBUtl.GetTable(sql);
        return View(dt.Rows);
    }

    #region "Product Add"
    public IActionResult ProductAdd()
    {
        return View();
    }

    public IActionResult ProductAddPost()
    {
        IFormCollection form = HttpContext.Request.Form;
        string orgDesc = form["Description"].ToString().Trim();
        string orgPrice = form["Price"].ToString().Trim();
        string orgGram = form["Weight"].ToString().Trim();
        string orgCountry = form["Country"].ToString().Trim();

        string sql = @"INSERT INTO OrgProduct(OrgDesc, Price, Gram, Country) 
                        VALUES('{0}', {1}, {2}, '{3}')";
        string insert = String.Format(sql, orgDesc, orgPrice, orgGram, orgCountry);
        int res = DBUtl.ExecSQL(insert);
        if (res == 1)
        {
            TempData["Message"] = "Product Added";
            TempData["MsgType"] = "success";
        }
        else
        {
            TempData["Message"] = DBUtl.DB_Message;
            TempData["ExecSQL"] = DBUtl.DB_SQL;
            TempData["MsgType"] = "danger";
        }
        return RedirectToAction("Products");
    }
    #endregion

    #region "Product Edit"
    public IActionResult ProductEdit(string id)
    {
        string sql = "SELECT * FROM OrgProduct WHERE OrgCode={0}";
        string select = String.Format(sql, id);
        DataTable dt = DBUtl.GetTable(select);
        if (dt.Rows.Count == 1)
        {
            OrgProduct product = new()
            {
                OrgCode = (int)dt.Rows[0]["OrgCode"],
                Gram = (int)dt.Rows[0]["Gram"],
                Price = (double)dt.Rows[0]["Price"],
                OrgDesc = dt.Rows[0]["OrgDesc"].ToString()!,
                Country = dt.Rows[0]["Country"].ToString()!
            };
            return View(product);
        }
        else
        {
            TempData["Message"] = "Product Not Found";
            TempData["MsgType"] = "warning";
            return RedirectToAction("Products");
        }
    }

    public IActionResult ProductEditPost()
    {
        IFormCollection form = HttpContext.Request.Form;
        string orgCode = form["Pcode"].ToString().Trim();
        string orgDesc = form["Description"].ToString().Trim();
        string orgCountry = form["Country"].ToString().Trim();
        string orgPrice = form["Price"].ToString().Trim();
        string orgGram = form["Weight"].ToString().Trim();

        string sql = @"UPDATE OrgProduct
                    SET OrgDesc = '{1}',
                        Price   = {2},
                        Gram    = {3},
                        Country = '{4}'
                    WHERE OrgCode = {0}";

        string update = String.Format(sql, orgCode, orgDesc, orgPrice, orgGram, orgCountry);
        int count = DBUtl.ExecSQL(update);

        if (count == 1)
        {
            TempData["Message"] = "Product successfully modified.";
            TempData["MsgType"] = "success";
        }
        else
        {
            TempData["Message"] = DBUtl.DB_Message;
            TempData["ExecSQL"] = DBUtl.DB_SQL;
            TempData["MsgType"] = "danger";
        }
        return RedirectToAction("Products");
    }
    #endregion

    #region "Product Delete"
    public IActionResult ProductDelete(string id)
    {
        string sql = "DELETE FROM OrgProduct WHERE OrgCode={0}";
        string delete = String.Format(sql, id);
        int res = DBUtl.ExecSQL(delete);
        if (res == 1)
        {
            TempData["Message"] = "Product Deleted";
            TempData["MsgType"] = "success";
        }
        else
        {
            TempData["Message"] = DBUtl.DB_Message;
            TempData["MsgType"] = "danger";
        }
        return RedirectToAction("Products");
    }
    #endregion

    #region "Subscription"

    public IActionResult SubscriptionList()
    {
        string sql = "SELECT * FROM OrgSubscription";
        DataTable dt = DBUtl.GetTable(sql);
        return View(dt.Rows);
    }

    public IActionResult Subscription()
    {
        return View("Subscription");
    }

    public IActionResult Confirmation()
    {
        IFormCollection form = HttpContext.Request.Form;
        string name = form["Name"].ToString().Trim();
        string email = form["Email"].ToString().Trim();
        string refer = form["Refer"].ToString().Trim();
        string comments = form["Comments"].ToString().Trim();
        string gender = form["Gender"].ToString();

        // Read checkBoxes  
        string recipe = form["Recipe"].ToString();
        string news = form["News"].ToString();
        string offers = form["Offers"].ToString();

        // Get CheckBoxes for Interest
        string interest = "";
        if (recipe.Equals("Recipe"))
            interest += "Recipe, ";
        if (news.Equals("News"))
            interest += "News, ";
        if (offers.Equals("Offers"))
            interest += "Offers, ";
        if (!interest.Equals(""))
            interest = interest[0..^2];

        // Add Record to Database
        string sql = @"INSERT INTO OrgSubscription
                          (UserName, SubEmail, Gender, Recipes, 
                           News, Offers, Referral, Comments)
                        VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";

        string insert = String.Format(sql, name, email,
                                           gender[..1],  // F or M
                                           recipe.Equals("Recipe"), // true or false
                                           news.Equals("News"),
                                           offers.Equals("Offers"),
                                           refer, comments);

        int rowsAffected = DBUtl.ExecSQL(insert);

        // Check Insert is Successful
        if (rowsAffected == 1)
        {
            Subscription s = new()
            {
                UserName = name,
                SubEmail = email,
                Gender = gender,
                Interest = interest,
                Referral = refer,
                Comments = comments
            };
            return View("Confirmation", s);
        }
        else
        {
            ViewData["Message"] = DBUtl.DB_Message;
            return View("Subscription");
        }
    }

    public IActionResult SubscriptionDelete(string id)
    {
        string sql = "DELETE FROM OrgSubscription WHERE Sno={0}";
        string delete = String.Format(sql, id);
        int res = DBUtl.ExecSQL(delete);
        if (res == 1)
        {
            TempData["Message"] = "Product Deleted";
            TempData["MsgType"] = "success";
        }
        else
        {
            TempData["Message"] = DBUtl.DB_Message;
            TempData["MsgType"] = "danger";
        }
        return RedirectToAction("SubscriptionList");
    }
    #endregion

}
