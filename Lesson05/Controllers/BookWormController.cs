using Microsoft.AspNetCore.Mvc;

namespace Lesson05.Controllers;

public class BookWormController : Controller
{
    

    #region "Books"
    public IActionResult Books()
    {
        string select =
           @"SELECT Isbn AS [ISBN],
                     Title,
                     Lang AS [Language],
                     Price,
                     Qty AS [Quantity],
                     BkType AS [Book Type]
                FROM BwBook";
        DataTable dt = DBUtl.GetTable(select);
        return View(dt);
    }

    public IActionResult BookAdd()
    {
        // TODO: L05 Task 3 Complete BookAdd Action
        // Remove the following line after writing your code
        return View();
    }

    public IActionResult BookAddPost()
    {
        // TODO: L05 Task 4 Complete BookAddPost Action
        // Remove the following line after writing your code
        IFormCollection form = HttpContext.Request.Form;
        string isbn = form["Isbn"].ToString().Trim();
        string title = form["Title"].ToString().Trim();
        string lang = form["Lang"].ToString().Trim();
        string price = form["Price"].ToString().Trim();
        string qty = form["Qty"].ToString().Trim();
        string bkType = form["Bktype"].ToString().Trim();
        string pubid = form["Pubid"].ToString().Trim();

        if (ValidUtl.CheckIfEmpty(isbn, title, lang , price , qty,bkType,pubid))
        {
            ViewData["Message"] = "Please enter all fields.";
            ViewData["MsgType"] = "warning";
            return View("BookAdd");
        }

        if (!pubid.IsInteger())
        {
            ViewData["Message"] = "Qty must be an integer.";
            ViewData["MsgType"] = "warning";
            return View("BookAdd");
        }

        if (!pubid.IsNumeric())
        {
            ViewData["Message"] = "Price must be numeric.";
            ViewData["MsgType"] = "warning";
            return View("BookAdd");
        }
        string sql =
           @"INSERT INTO BwBook(Isbn, Title, Lang , Price , Qty,BkType,Pubid)
              VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";

        string insert = String.Format(sql, isbn, title, lang, price, qty, bkType, pubid);

        int count = DBUtl.ExecSQL(insert);
        if (count == 1)
        {
            TempData["Message"] = "Book Successfully Added.";
            TempData["MsgType"] = "success";
            return RedirectToAction("Books");
        }
        else
        {
            ViewData["Message"] = DBUtl.DB_Message;
            ViewData["ExecSQL"] = DBUtl.DB_SQL;
            ViewData["MsgType"] = "danger";
            return View("BookAdd");
        }
       ;
    }

    public IActionResult BookDelete()
    {
        // TODO: L05 Task 5 Complete BookDelete Action
        // Remove the following line after writing your code
        return View("BookDelete");
    }

    public IActionResult BookDeletePost()
    {
        // TODO: L05 Task 6 Complete BookDeletePost Action
        // Remove the following line after writing your code
        IFormCollection form = HttpContext.Request.Form;
        string isbn = form["Isbn"].ToString().Trim();
        if (ValidUtl.CheckIfEmpty(isbn))
        {
            ViewData["Message"] = "Please enter Isbn";
            ViewData["MsgType"] = "warning";
            return View("BookDelete");
        }

 

        string sql = @"SELECT * FROM BwBook WHERE Isbn={0}";
        string select = String.Format(sql, isbn);
        DataTable dt = DBUtl.GetTable(select);
        if (dt.Rows.Count == 0)
        {
            ViewData["Message"] = "Isbn Not Found. " + isbn;
            ViewData["MsgType"] = "warning";
            return View("BookDelete");
        }

 

        sql = @"DELETE BwBook WHERE Isbn='{0}'";
        string delete = String.Format(sql, isbn);
        int count = DBUtl.ExecSQL(delete);
        if (count == 1)
        {
            ViewData["Message"] = "Book Deleted.";
            ViewData["MsgType"] = "success";
        }
        else
        {
            ViewData["Message"] = DBUtl.DB_Message;
            ViewData["ExecSQL"] = DBUtl.DB_SQL;
            ViewData["MsgType"] = "danger";
        }

 

        return View("BookDelete");

 

    }
    
    #endregion

    #region "Publishers"
    public IActionResult Publishers()
    {
        string select =
           @"SELECT PubId   AS [ID],
                     PubName AS [Publisher Name],
                     PubAddr AS [Address]
                FROM BwPublisher";
        DataTable dt = DBUtl.GetTable(select);
        return View(dt);
    }

    public IActionResult PubAdd()
    {
        return View();
    }

    public IActionResult PubAddPost()
    {
        IFormCollection form = HttpContext.Request.Form;
        string pubid = form["Pubid"].ToString().Trim();
        string pubname = form["Pubname"].ToString().Trim();
        string pubaddr = form["Pubaddr"].ToString().Trim();

        if (ValidUtl.CheckIfEmpty(pubid, pubname, pubaddr))
        {
            ViewData["Message"] = "Please enter all fields.";
            ViewData["MsgType"] = "warning";
            return View("PubAdd");
        }

        if (!pubid.IsInteger())
        {
            ViewData["Message"] = "Pub ID must be an integer.";
            ViewData["MsgType"] = "warning";
            return View("PubAdd");
        }

        string sql =
           @"INSERT INTO BwPublisher(PubID, PubName, PubAddr)
              VALUES({0},'{1}','{2}')";

        string insert = String.Format(sql, pubid, pubname, pubaddr);

        int count = DBUtl.ExecSQL(insert);
        if (count == 1)
        {
            TempData["Message"] = "Publisher Successfully Added.";
            TempData["MsgType"] = "success";
            return RedirectToAction("Publishers");
        }
        else
        {
            ViewData["Message"] = DBUtl.DB_Message;
            ViewData["ExecSQL"] = DBUtl.DB_SQL;
            ViewData["MsgType"] = "danger";
            return View("PubAdd");
        }
    }

    public IActionResult PubDelete()
    {
        return View("PubDelete");
    }

    public IActionResult PubDeletePost()
    {
        IFormCollection form = HttpContext.Request.Form;
        string pubid = form["Pubid"].ToString().Trim();

        if (!pubid.IsInteger())
        {
            ViewData["Message"] = "Pub ID must be an integer.";
            ViewData["MsgType"] = "warning";
            return View("PubDelete");
        }

        string sql = @"SELECT * FROM BwPublisher WHERE PubID={0}";
        string select = String.Format(sql, pubid);
        DataTable dt = DBUtl.GetTable(select);
        if (dt.Rows.Count == 0)
        {
            ViewData["Message"] = "Pub ID Not Found.";
            ViewData["MsgType"] = "warning";
            return View("PubDelete");
        }

        sql = @"DELETE BwPublisher WHERE PubID='{0}'";
        string delete = String.Format(sql, pubid);
        int count = DBUtl.ExecSQL(delete);
        if (count == 1)
        {
            ViewData["Message"] = "Publisher Deleted.";
            ViewData["MsgType"] = "success";
        }
        else
        {
            ViewData["Message"] = DBUtl.DB_Message;
            ViewData["ExecSQL"] = DBUtl.DB_SQL;
            ViewData["MsgType"] = "danger";
        }

        return View("PubDelete");
    }
    #endregion
}
// 22036043 Yeap Ruo Han