using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Lesson13.Controllers;

public class ChartController : Controller
{

    // TODO: Lesson13 Task 1 - Add security annotations to all actions
    [Authorize(Roles = "manager")]
    public IActionResult Duration()
    {
        PrepareData(1);
        ViewData["Chart"] = "doughnut";
        ViewData["Title"] = "Duration";
        ViewData["ShowLegend"] = "true";
        ViewData["LabelColor"] = "black";
        return View("Chart");
    }
    [Authorize(Roles = "manager")]
    public IActionResult Genre()
    {
        PrepareData(2);
        ViewData["Chart"] = "line";
        ViewData["Title"] = "Genre";
        ViewData["ShowLegend"] = "false";
        ViewData["LabelColor"] = "black";
        return View("Chart");
    }
    [Authorize(Roles = "manager")]
    public IActionResult Price()
    {
        PrepareData(3);
        ViewData["Chart"] = "bar";
        ViewData["Title"] = "Price";
        ViewData["ShowLegend"] = "false";
        ViewData["LabelColor"] = "white";
        return View("Chart");
    }

    private void PrepareData(int x)
    {
        List<Movie> list = DBUtl.GetList<Movie>("SELECT * FROM Movie");

        // Initialize genre array with zeros, based on the number of genres in the database
        List<Genre> genres = DBUtl.GetList<Genre>("SELECT GenreName FROM Genre");
        int[] genre = new int[genres.Count];

        int[] duration = new int[5];
        int[] price = new int[3];

        foreach (Movie movie in list)
        {
            // Duration logic
            if (movie.Duration < 100) duration[0]++;
            else if (movie.Duration < 120) duration[1]++;
            else if (movie.Duration < 140) duration[2]++;
            else if (movie.Duration < 160) duration[3]++;
            else duration[4]++;

            // Genre logic
            if (movie.GenreId >= 1 && movie.GenreId <= genre.Length)
            {
                genre[movie.GenreId - 1]++;
            }

            // Price logic
            if (movie.Price < 14) price[0]++;
            else if (movie.Price < 17) price[1]++;
            else price[2]++;
        }

        if (x == 1)
        {
            ViewData["Legend"] = "Movies by Duration";
            ViewData["Colors"] = new[] { "#D3F7FD", "#A3D5FF", "#83C9F4", "#6F73D2", "#7681B3" };
            ViewData["Labels"] = new[] { "Very Short", "Short", "Normal", "Long", "Very Long" };
            ViewData["Data"] = duration;
        }
        else if (x == 2)
        {
            ViewData["Legend"] = "Movies by Genre";
            ViewData["Colors"] = new[] { "#2C365E", "#484D6D", "#4B8F8C", "#C5979D" };
            ViewData["Labels"] = genres.Select(g => g.GenreName).ToArray();
            ViewData["Data"] = genre;
        }
        else if (x == 3)
        {
            ViewData["Legend"] = "Movies by Price";
            ViewData["Colors"] = new[] { "#42E2B8", "#2D82B7", "#EB8A90" };
            ViewData["Labels"] = new[] { "Discounted", "Normal", "Premium" };
            ViewData["Data"] = price;
        }
        else
        {
            ViewData["Legend"] = "Nothing";
            ViewData["Colors"] = new[] { "red", "yellow", "black" };
            ViewData["Labels"] = new[] { "X", "Y", "Z" };
            ViewData["Data"] = new int[] { 0, 0, 0 };
        }
    }

}
