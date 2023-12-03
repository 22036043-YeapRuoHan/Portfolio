using Microsoft.AspNetCore.Mvc;



namespace Lesson04.Controllers
{
    public class BookWormController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        const string SELECT1 = // QUESTION ONE
                @"SELECT Isbn, Title, Lang
         FROM BwBook
         WHERE Lang != 'English'";



        const string SELECT2 = // QUESTION TWO
                @"SELECT Isbn, Title, Qty
         FROM BwBook
         WHERE Qty = 0";



        const string SELECT3 = // QUESTION THREE
                @"SELECT P.PubName AS Publisher, COUNT(*) AS Titles
         FROM BwPublisher P
         INNER JOIN BwBook B 
            ON B.PubID = P.PubID
         GROUP BY P.PubName";



        const string SELECT4 = // QUESTION FOUR
                @"SELECT Title
         FROM BwBook
         GROUP BY title
         HAVING COUNT(Lang) > 1";



        public IActionResult Query()
        {
            return View();
        }



        public IActionResult Submit()
        {
            IFormCollection form = HttpContext.Request.Form;
            string question = form["Question"].ToString();



            string sql = "";
            if (question.Equals("1"))
            {
                sql = SELECT1;
            }
            else if (question.Equals("2"))
            {
                sql = SELECT2;
            }
            else if (question.Equals("3"))
            {
                sql = SELECT3;
            }
            else if (question.Equals("4"))
            {
                sql = SELECT4;
            }



            DataTable dt = DBUtl.GetTable(sql);
            if (dt.Rows.Count == 0)
            {
                ViewData["Message"] = DBUtl.DB_Message;
                ViewData["ExecSQL"] = DBUtl.DB_SQL;



            }
            return View("Query", dt);



        }



    }
}
//22036043 Yeap Ruo Han 
