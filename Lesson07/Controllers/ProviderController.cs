using Microsoft.AspNetCore.Mvc;

namespace Lesson07.Controllers;

public class ProviderController : Controller
{
    #region "Providers" 
    public IActionResult Index()
    {
        return View("Index");
    }

    public IActionResult Providers()
    {
        string select = "SELECT * FROM Provider";
        DataTable dt = DBUtl.GetTable(select);
        return View(dt.Rows);
    }
    #endregion

    #region "Provider Add" 
    // TODO: Lesson07 Task 1 - Complete the ProviderAdd() action
    public IActionResult ProviderAdd()
    {
        return View();
    }
    // TODO: Lesson07 Task 2 - Complete the ProviderAddPost() action.
    public IActionResult ProviderAddPost()
    {
        // Retrieve the text data from the form
        IFormCollection form = HttpContext.Request.Form;
        string providername = form["providername"].ToString().Trim();
        // Write the SQL to insert the record into the database
        string sql = @"INSERT INTO Provider (name) VALUES ('{0}')";
        string insert = String.Format(sql, providername.EscQuote());
        // Execute the SQL
        int res = DBUtl.ExecSQL(insert);
        // Check if the SQL was successful. 
        //     If successful redirect to the 'Providers' action
        //     If failure show the DB_Message and return to the 'ProviderAdd' view
        if (res == 1)
        {
            TempData["Message"] = "Provider Record Added";
            TempData["MsgType"] = "success";
            return RedirectToAction("Providers");
        }
        else
        {
            ViewData["Message"] = DBUtl.DB_Message;
            ViewData["ExecSQL"] = DBUtl.DB_SQL;
            ViewData["MsgType"] = "danger";
            return View("ProviderAdd");
        }
        // Delete this line when Task 2 complete.
    }
    #endregion

    #region "Provider Delete"        
    public IActionResult ProviderDelete(string id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        string sql = "SELECT * FROM Provider WHERE provider_id={0}";
        string select = string.Format(sql, id);
        DataTable dt = DBUtl.GetTable(select);
        if (dt.Rows.Count == 0)
        {
            TempData["Message"] = "Provider Not Found";
            TempData["MsgType"] = "warning";
            return RedirectToAction("Providers");
        }

        // Check the selected provider doesn't have any subscriptions
        sql = "SELECT * FROM Subscription WHERE provider_id={0}";
        select = string.Format(sql, id);
        DataTable dt2 = DBUtl.GetTable(select);
        if (dt2.Rows.Count > 0)
        {
            TempData["Message"] = @"Provider cannot be deleleted. 
                                      All of the providers's subscriptions must be deleted first";
            TempData["MsgType"] = "warning";
            return RedirectToAction("Providers");
        }

        sql = @"DELETE Provider WHERE provider_id ={0}";
        string delete = string.Format(sql, id);

        int count = DBUtl.ExecSQL(delete);
        if (count == 1)
        {
            TempData["Message"] = "Provider Record Deleted";
            TempData["MsgType"] = "success";
        }
        else
        {
            TempData["Message"] = DBUtl.DB_Message;
            TempData["ExecSQL"] = DBUtl.DB_SQL;
            TempData["MsgType"] = "danger";
        }
        return RedirectToAction("Providers");
    }
    #endregion

    #region "Provider Edit"
    public IActionResult ProviderEdit(string id)
    {
        string sql = "SELECT * FROM Provider WHERE provider_id={0}";
        string select = string.Format(sql, id);
        DataTable dt = DBUtl.GetTable(select);
        if (dt.Rows.Count == 1)
        {
            Provider prov = new()
            {
                ProviderId = (int)dt.Rows[0]["provider_id"],
                Name = dt.Rows[0]["name"].ToString() ?? "",
            };
            return View(prov);
        }
        else
        {
            TempData["Message"] = "Provider Not Found";
            TempData["MsgType"] = "warning";
            return RedirectToAction("Providers");
        }
    }

    public IActionResult ProviderEditPost()
    {
        IFormCollection form = HttpContext.Request.Form;
        string provId = form["provId"].ToString().Trim();
        string provName = form["provName"].ToString().Trim();

        // Update Record in Database  
        string sql = @"UPDATE Provider
                       SET name = '{1}'
                       WHERE Provider_id={0}";

        string update = string.Format(sql, provId, provName);

        int count = DBUtl.ExecSQL(update);
        if (count == 1)
        {
            TempData["Message"] = "Provider Record Updated";
            TempData["MsgType"] = "success";
            return RedirectToAction("Providers");
        }
        else
        {
            ViewData["Message"] = DBUtl.DB_Message;
            ViewData["ExecSQL"] = DBUtl.DB_SQL;
            ViewData["MsgType"] = "danger";
            Provider prov = new()
            {
                ProviderId = int.Parse(provId),
                Name = provName,
            };
            return View("ProviderEdit", prov);
        }
    }
    #endregion
}
// 22036043 Yeap Ruo Han