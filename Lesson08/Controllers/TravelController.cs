using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lesson08.Controllers;

public class TravelController : Controller
{
    // TODO Lesson 08 Task 2a - Specify Authorisation for Index
    [AllowAnonymous] 
    public IActionResult Index()
    {
        
        DataTable dt = DBUtl.GetTable("SELECT * FROM TravelHighlight");
        _ = User.Identity!.IsAuthenticated;
        return View("Index", dt.Rows);
    }

    // TODO Lesson 08 Task 2b - Specify Authorisation for Details
     [AllowAnonymous] 
    public IActionResult Details(int id)
    {
        string sql =
           @"SELECT h.*, u.FullName AS SubmittedBy
             FROM TravelHighlight h, TravelUser u
             WHERE h.UserId = u.UserId
             AND Id={0}";

        string select = string.Format(sql, id);
        List<Trip> lstTrip = DBUtl.GetList<Trip>(select);
        if (lstTrip.Count == 1)
        {
            Trip trip = lstTrip[0];
            return View("Details", trip);
        }
        else
        {
            TempData["Message"] = "Trip Record does not exist";
            TempData["MsgType"] = "warning";
            return RedirectToAction("Index");
        }
    }

    // TODO Lesson 08 Task 2c - Specify Authorisation for MyTrips
    [Authorize]
    public IActionResult MyTrips()
    {
        // TODO Lesson 08 Task 4 - Complete the MyTrips method
        
      string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value; 
        string select = string.Format(@"SELECT * FROM TravelHighlight 
                                        WHERE UserId = '{0}'", userid);
        List<Trip> list = DBUtl.GetList<Trip>(select);
        return View("MyTrips", list);
         
    }

    // TODO Lesson 08 Task 2d - Specify Authorisation for Create
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    // TODO Lesson 08 Task 2e - Specify Authorisation for Create Post
    
    [HttpPost]
    [Authorize]
    //public IActionResult Create(Trip trip, IFormFile photo)
    public IActionResult Create(Trip trip)
    {
        // TODO Lesson 08 - Task 5 - Complete the Create Post method.

        ModelState.Remove("Picture");     // No Need to Validate "Picture" - derived from "Photo".
        ModelState.Remove("SubmittedBy"); // Ignore "SubmittedBy". See claim below.

        if (!ModelState.IsValid)
        {
            return View("Create");
        }
        else
        {
            string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            string picfilename = DoPhotoUpload(trip.Photo);
            string sql = @"INSERT INTO  TravelHighlight (Title,City,TripDate,
                                  Duration,Spending,Story,picture,UserId)
                         VALUES('{0}','{1}','{2:yyyy-MM-dd}','{3}','{4}','{5}','{6}','{7}')";

            string insert = string.Format(sql, trip.Title.EscQuote(), trip.City.EscQuote(), trip.TripDate,trip.Duration, trip.Spending, trip.Story.EscQuote(),picfilename,userid);

            if (DBUtl.ExecSQL(insert) == 1)
            {
                TempData["Message"] = "Trip Successfully Added.";
                TempData["MsgType"] = "success";
                return RedirectToAction("MyTrips");
            }
            else
            {
                ViewData["Message"] = DBUtl.DB_Message;
                ViewData["ExecSQL"] = DBUtl.DB_SQL;
                ViewData["MsgType"] = "danger";
                return View("Create");
            }
        }
         // Remove this line when Task 5 completed.
    }

    // TODO Lesson 08 Task 2f - Specify Authorisation for Update
     [Authorize]
    public IActionResult Update(int id)
    {
        string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        string sql = @"SELECT * FROM TravelHighlight 
                       WHERE Id={0} AND UserId='{1}'";

        string select = string.Format(sql, id, userid);
        List<Trip> lstTrip = DBUtl.GetList<Trip>(select);
        if (lstTrip.Count == 1)
        {
            Trip trip = lstTrip[0];
            return View(trip);
        }
        else
        {
            TempData["Message"] = "Trip Record does not exist";
            TempData["MsgType"] = "warning";
            return RedirectToAction("MyTrips");
        }
    }

    // TODO Lesson 08 Task 2g - Specify Authorisation for Update Post
    
    [HttpPost]
    [Authorize]
    public IActionResult Update(Trip trip)
    {
        ModelState.Remove("Photo");
        ModelState.Remove("SubmittedBy");// No Need to Validate "Photo"
        if (!ModelState.IsValid)
        {
            return View("Update", trip);
        }
        else
        {
            string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            string sql = @"UPDATE TravelHighlight  
                              SET Title='{2}', City='{3}', Story='{4}',
                                  TripDate='{5:yyyy-MM-dd}', Duration={6}, Spending={7} 
                            WHERE Id={0} AND UserId='{1}'";
            string update = string.Format(sql, trip.Id, userid,
                                          trip.Title.EscQuote(),
                                          trip.City.EscQuote(),
                                          trip.Story.EscQuote(),
                                          trip.TripDate, trip.Duration, trip.Spending);
            if (DBUtl.ExecSQL(update) == 1)
            {
                TempData["Message"] = "Trip Updated";
                TempData["MsgType"] = "success";
            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                ViewData["ExecSQL"] = DBUtl.DB_SQL;
                TempData["MsgType"] = "danger";
            }
            return RedirectToAction("MyTrips");
        }
    }

    // TODO Lesson 08 Task 2h - Specify Authorisation for Delete
    [Authorize]
    public IActionResult Delete(int id)
    {
        string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        string sql = @"SELECT * FROM TravelHighlight 
                       WHERE id={0} AND UserId='{1}'";
        string select = string.Format(sql, id, userid);
        DataTable ds = DBUtl.GetTable(select);
        if (ds.Rows.Count != 1)
        {
            TempData["Message"] = "Trip Record does not exist";
            TempData["MsgType"] = "warning";
        }
        else
        {
            string photoFile = ds.Rows[0]["picture"].ToString()!;
            string fullpath = Path.Combine(_env.WebRootPath, "photos/" + photoFile);
            System.IO.File.Delete(fullpath);

            int res = DBUtl.ExecSQL(string.Format("DELETE FROM TravelHighlight WHERE id={0}", id));
            if (res == 1)
            {
                TempData["Message"] = "Trip Record Deleted";
                TempData["MsgType"] = "success";
            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["ExecSQL"] = DBUtl.DB_SQL;
                TempData["MsgType"] = "danger";
            }
        }
        return RedirectToAction("MyTrips");
    }

    private string DoPhotoUpload(IFormFile photo)
    {
        string fext = Path.GetExtension(photo.FileName);
        string uname = Guid.NewGuid().ToString();
        string fname = uname + fext;
        string fullpath = Path.Combine(_env.WebRootPath, "photos/" + fname);
        using (FileStream fs = new(fullpath, FileMode.Create))
        {
            photo.CopyTo(fs);
        }
        return fname;
    }

    private readonly IWebHostEnvironment _env;
    public TravelController(IWebHostEnvironment environment)
    {
        _env = environment;
    }

}
// 22036043 Yeap Ruo Han
