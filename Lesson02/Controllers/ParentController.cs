using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Lesson02.Controllers
{
    public class ParentController : Controller
    {
        private bool CheckIfEmpty(params String[] list)
        { foreach(String o in list) {
                if (o ==null || o.Trim().Equals("")) {
                    return true;
                }
            } 
        return false;
        }   
        private static int CalcCreditPoint(string activity, int days)
        {
            int credits = 0;
            if (activity.Equals("Story Telling") ||
                activity.Equals("Art and Craft"))
            {
                credits = days * 10;
            }
            else if (activity.Equals("Traffic Control"))
            {
                credits = days * 5;
            }
            else if (activity.Equals("Music Appreciation"))
            {
                credits = days * 15;
            }
            return credits;
        }

        public IActionResult Volunteer()
        {
            return View();
        }

        public IActionResult Submit()
        {
            // Use IFormCollection for shorter coding
            IFormCollection form = HttpContext.Request.Form;

            // Read the RadioButtons 
            string title = form["Title"].ToString();

            // Read the TextFields
            string name = form["Name"].ToString().Trim();
            string mobile = form["Mobile"].ToString().Trim();
            string postal = form["Postal"].ToString().Trim();
            // ....

            // Read the drop-down list
            string activity = form["Activity"].ToString();

            // Read the CheckBoxes 
            string mon = form["Mon"].ToString();
            string wed = form["Wed"].ToString();
            string fri = form["Fri"].ToString();
            // ....
            if (CheckIfEmpty(name, postal, mobile, activity))
            {
                ViewData["Message"] = "Please enter all fields";
                return View("Volunteer");
            }

            // Determine Number of Days Checked
            int days = 0;
            string daysSelected = "";
            if (mon.Equals("Mon"))
            {
                days++;
                daysSelected+="Monday, ";
            }

            if (mon.Equals("Wed"))
            {
                days++;
                daysSelected+="Wednesday, ";
            }

            if (mon.Equals("Fri"))
            {
                days++;
                daysSelected+="Friday, ";
            }

            if (days==0)
            {
                ViewData["Message"] = "Please check Days";
                return View("Volunteer");
            }
            daysSelected = daysSelected.Substring(0, daysSelected.Length -2);

            // Passing Data to the View
            ViewData["Fullname"] = title + " " + name;
            ViewData["activity"] = activity;
            ViewData["days"] = daysSelected;
            ViewData["credits"] = CalcCreditPoint(activity, days);

            return View(); // Submit.cshtml
        }

    }





}//22036043 Yeap Ruo Han
