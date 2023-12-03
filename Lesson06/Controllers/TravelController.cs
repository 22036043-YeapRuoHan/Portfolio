﻿using Microsoft.AspNetCore.Mvc;

namespace Lesson06;

public class TravelController : Controller
{
    private readonly IWebHostEnvironment _env;

    public TravelController(IWebHostEnvironment environment)
    {
        _env = environment;
    }

    public IActionResult Index()
    {
        DataTable dt = DBUtl.GetTable("SELECT * FROM Travel");
        return View("Main", dt.Rows);
    }

    public IActionResult Add()
    {
        return View();
    }

    public IActionResult AddPost(IFormFile photo)
    {
        // TODO: Lesson06 Task 3a - Get Values Posted from the View
        IFormCollection form = HttpContext.Request.Form;
        string title = form["Title"].ToString().Trim();
        string city = form["City"].ToString().Trim();
        string story = form["Story"].ToString().Trim();
        string tripDate = form["TripDate"].ToString().Trim();
        string duration = form["Duration"].ToString().Trim();
        string spending = form["Cost"].ToString().Trim();
        // TODO: Lesson06 Task 3b - Call Method to Upload Photo
        string picfilename = DoPhotoUpload(photo!);

        // TODO: Lesson06 Task 3c - Insert record into travel table
        string sql = @"INSERT INTO Travel(Title, City, TripDate, Duration, Spending, Story, Picture)
                        VALUES('{0}','{1}','{2}',{3},{4},'{5}','{6}')";

        string insert = String.Format(sql, title.EscQuote(), city.EscQuote(), tripDate, duration, spending,
            story.EscQuote(), picfilename);

        if (DBUtl.ExecSQL(insert) == 1)
        {
            TempData["Message"] = "Trip Successfully Added.";
            TempData["MsgType"] = "success";
            return RedirectToAction("Index");
        }
        else
        {
            ViewData["Message"] = DBUtl.DB_Message;
            ViewData["MsgType"] = "danger";
            return View("Add");
        }
    }

    public IActionResult Edit(string id)
    {
        if (id == null)
            return RedirectToAction("Index");

        string sql = String.Format("SELECT * FROM Travel WHERE id = {0}", id);
        DataTable ds = DBUtl.GetTable(sql);
        if (ds.Rows.Count == 1)
        {
            Trip trip = new()
            {
                ID = Int32.Parse(id),
                Title = ds.Rows[0]["Title"].ToString()!,
                City = ds.Rows[0]["City"].ToString()!,
                Story = ds.Rows[0]["Story"].ToString()!,
                TripDate = (DateTime)ds.Rows[0]["TripDate"],
                Duration = (int)ds.Rows[0]["Duration"],
                Spending = (double)ds.Rows[0]["Spending"],
                PhotoFile = ds.Rows[0]["picture"].ToString()!
            };
            return View(trip);
        }
        else
        {
            TempData["Message"] = "Trip Record does not exist";
            TempData["MsgType"] = "warning";
            return RedirectToAction("Index");
        }
    }

    public IActionResult EditPost()
    {
        // TODO: Lesson06 Task 5a - Get Values Posted from the View
        IFormCollection form = HttpContext.Request.Form;
        string id = form["ID"].ToString().Trim();    // Hidden Field
        string story = form["Story"].ToString().Trim();

        // TODO: Lesson06 Task 5b - Update Record in Travel Table
        string sql = "UPDATE Travel SET Story='{1}' WHERE Id={0}";
        sql = String.Format(sql, id, story.EscQuote());
        if (DBUtl.ExecSQL(sql) == 1)
        {
            TempData["Message"] = "Trip Updated";
            TempData["MsgType"] = "success";
        }
        else
        {
            TempData["Message"] = DBUtl.DB_Message;
            TempData["MsgType"] = "danger";
        }
        return RedirectToAction("Index");
    }

    public IActionResult Delete(string id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        string sql = String.Format("SELECT * FROM Travel WHERE id={0}", id);
        DataTable ds = DBUtl.GetTable(sql);
        if (ds.Rows.Count != 1)
        {
            TempData["Message"] = "Trip Record does not exist";
            TempData["MsgType"] = "warning";
        }
        else
        {
            string photoFile = ds.Rows[0]["picture"].ToString()!;
            string fullpath = Path.Combine(_env.WebRootPath, "photos/" + photoFile);

            // TODO: Lesson06 Task 6a - Delete the Photo from the Web Server
            System.IO.File.Delete(fullpath);

            // TODO: Lesson06 Task 6b - Delete Record From Travel Table
            int res = DBUtl.ExecSQL(String.Format("DELETE FROM Travel WHERE id={0}", id));
            if (res == 1)
            {
                TempData["Message"] = "Trip Record Deleted";
                TempData["MsgType"] = "success";
            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["MsgType"] = "danger";
            }

        }
        return RedirectToAction("Index");
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
}

// 22036043 Yeap Ruo Han