using Microsoft.AspNetCore.Mvc;

namespace Lesson07.Controllers;

public class SubscriptionController : Controller
{
    #region "Subscriptions" 
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Subscriptions()
    {
        string select =
          @"SELECT *
                FROM Subscription, Subscriber, Provider
                WHERE Subscription.subscriber_id = Subscriber.subscriber_id
                AND Subscription.provider_id = Provider.provider_id
                ORDER BY subscription_id";

        DataTable dt = DBUtl.GetTable(select);
        return View(dt.Rows);
    }
    #endregion

    #region Subscription Add
    private void PopulateViewData()
    {
        DataTable dt1 = DBUtl.GetTable("SELECT * FROM Provider");
        ViewData["Providers"] = dt1.Rows;

        DataTable dt2 = DBUtl.GetTable("SELECT * FROM Subscriber");
        ViewData["Subscribers"] = dt2.Rows;
    }

    public IActionResult SubscriptionAdd()
    {
        PopulateViewData();
        return View();
    }

    public IActionResult SubscriptionAddPost()
    {
        IFormCollection form = HttpContext.Request.Form;

        string sid = form["subscriber"].ToString().Trim();
        string pid = form["provider"].ToString().Trim();
        string subDate = form["datesub"].ToString().Trim();

        if (sid == "" || pid == "")
        {
            TempData["Message"] = @"No record was added, because
                                        valid values for provider and
                                        subscriber were not selected.";
            TempData["MsgType"] = "danger";
            return RedirectToAction("Subscriptions");
        }

        // Add Record to Database  
        string sql = @"INSERT INTO Subscription
                          (subscriber_id, provider_id, date_subscribed)
                         VALUES('{0}','{1}','{2}')";

        string insert = string.Format(sql, sid, pid, subDate);

        int count = DBUtl.ExecSQL(insert);
        if (count == 1)
        {
            TempData["Message"] = "Subscription Successfully Added.";
            TempData["MsgType"] = "success";
            return RedirectToAction("Subscriptions");
        }
        else
        {
            PopulateViewData();
            ViewData["Message"] = DBUtl.DB_Message;
            ViewData["ExecSQL"] = DBUtl.DB_SQL;
            ViewData["MsgType"] = "danger";
            return View("SubscriptionAdd");
        }
    }
    #endregion

    #region "Subscription Delete"
    // TODO: Lesson07 Task 6 - Complete the SubscriptionDelete(string id) action
    public IActionResult SubscriptionDelete(string id)
    {
        if (id == null)
        {
            return RedirectToAction("Subscriptions");
        }

        string sql = String.Format("SELECT * FROM Subscription WHERE subscription_id={0}", id);

        DataTable dt = DBUtl.GetTable(sql);
        if (dt.Rows.Count != 1)
        {
            TempData["Message"] = "Subscription Record Not Found";
            TempData["MsgType"] = "warning";

        }

        else
        {
            sql = "DELETE Subscription WHERE subscription_id={0}";
            string delete = string.Format(sql, id);

            int count = DBUtl.ExecSQL(delete);
            if (count == 1)
            {
                TempData["Message"] = "Subscription Record Deleted";
                TempData["MsgType"] = "success";
            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["MsgType"] = "danger";
            }
        }
        return RedirectToAction("Subscriptions");
    }


    #endregion

    #region "Subscription Edit"
    // TODO: Lesson07 Task 7 - Complete the SubscriptionEdit(string id) action

    public IActionResult SubscriptionEdit(string id)
    {
        string select = $"SELECT * FROM Subscription WHERE subscription_id={id}";

        DataTable dt = DBUtl.GetTable(select);
        if (dt.Rows.Count == 0)
        {
            TempData["Message"] = "Subscription Record Not Found";
            TempData["MsgType"] = "warning";
            return RedirectToAction("Subscription");
        }
        else
        {
            PopulateViewData();
            Subscription subp = new()
            {
                SubscriptionId = (int)dt.Rows[0]["subscription_id"],
                SubscriberId = (int)dt.Rows[0]["subscriber_id"],
                ProviderId = (int)dt.Rows[0]["provider_id"],

                DateSubscribed = (DateTime)dt.Rows[0]["date_subscribed"],

            };
            return View(subp);
        }
        // Remove this line when Task 7 complete.
    }

    // TODO: Lesson07 Task 8 - Create the SubscriptionEditPost() action
    public IActionResult SubscriptionEditPost()
    {
        IFormCollection form = HttpContext.Request.Form; 
        string sid = form["subscriberId"].ToString().Trim();
        string pid = form["providerId"].ToString().Trim(); 
        string subID = form["subscriptionId"].ToString().Trim();
        string subDate = form["dateSubscribed"].ToString().Trim();
        
        string sql = @"UPDATE Subscription SET subscriber_id = '{1}', provider_id = '{2}',date_subscribed ='{3}' WHERE subscription_id = '{0}'";
        string update = String.Format(sql, subID, sid, pid, subDate);
        int count = DBUtl.ExecSQL(update); 
        if (count == 1)
        {
            TempData["Message"] = "Subscription Updated"; 
            TempData["MsgType"] = "success";
        }
        else
        {
            TempData["Message"] = DBUtl.DB_Message;
            ViewData["ExecsOL"] = DBUtl.DB_SQL; 
            TempData["MsgType"] = "danger";
        }
        return RedirectToAction("Subscriptions");

    }

    #endregion
}
// 22036043 Yeap Ruo Han