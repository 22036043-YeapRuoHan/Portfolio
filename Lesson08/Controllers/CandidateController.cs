using Microsoft.AspNetCore.Mvc;

namespace Lesson08.Controllers;

public class CandidateController : Controller
{
    public IActionResult Index()
    {
        List<Candidate> lstCandidate =
           DBUtl.GetList<Candidate>("SELECT * FROM Candidate ORDER BY CName");
        return View(lstCandidate);
    }

    public IActionResult Display(int id)
    {
        string sql = string.Format(@"SELECT * FROM Candidate 
                                     WHERE RegNo = {0}", id);
        List<Candidate> lstCandidate = DBUtl.GetList<Candidate>(sql);
        if (lstCandidate.Count == 0)
        {
            TempData["Message"] = $"Candidate #{id} not found";
            TempData["MsgType"] = "warning";
            return RedirectToAction("Index");
        }
        else
        {
            // Get the FIRST element of the List
            Candidate cdd = lstCandidate[0];
            return View(cdd);
        }
    }

    // To Present An Empty Form
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // To Handle Post Back Input Data 
    [HttpPost]
    public IActionResult Create(Candidate cdd, IFormFile photo)
    {
        if (!ModelState.IsValid)
        {
            return View(cdd);
        }
        else
        {
            cdd.PicFile = Path.GetFileName(photo.FileName);
            string fname = "candidates/" + cdd.PicFile;
            UploadFile(photo, fname);

            string sql = @"INSERT Candidate(RegNo, CName, 
                                            Gender, Height, 
                                            BirthDate, Race, 
                                            Clearance, PicFile) 
                           VALUES({0},'{1}','{2}',{3},
                          '{4:yyyy-MM-dd}','{5}','{6}','{7}')";

            string insert =
               string.Format(sql, cdd.RegNo, cdd.CName, cdd.Gender,
                                  cdd.Height, cdd.BirthDate, cdd.Race,
                                  cdd.Clearance, cdd.PicFile);
            if (DBUtl.ExecSQL(insert) == 1)
            {
                TempData["Message"] = $"Candidate #{cdd.RegNo} created Successfully";
                TempData["MsgType"] = "success";
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["Message"] = DBUtl.DB_Message;
                ViewData["ExecSQL"] = DBUtl.DB_SQL;
                ViewData["MsgType"] = "danger";
                return View(cdd);
            }
        }
    }

    private string UploadFile(IFormFile ufile, string fname)
    {
        string fullpath = Path.Combine(_env.WebRootPath, fname);
        using (FileStream fs = new(fullpath, FileMode.Create))
        {
            ufile.CopyToAsync(fs);
        }
        return fname;
    }

    private readonly IWebHostEnvironment _env;

    public CandidateController(IWebHostEnvironment environment)
    {
        _env = environment;
    }

}
