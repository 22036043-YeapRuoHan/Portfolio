using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lesson12.Controllers;

public class WarehouseController : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        List<Warehouse> list = DBUtl.GetList<Warehouse>("SELECT * FROM Warehouse");
        return View(list);
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Update(int Id)
    {
        // Dummy action - Record is not actually updated
        // Placeholder code

        if (!ModelState.IsValid)
        {
            ViewData["Message"] = "Invalid Input";
            ViewData["MsgType"] = "warning";
            return View("EditAlias");
        }
        else
        {
            TempData["MsgType"] = "danger";
            TempData["Message"] = string.Format("Dummy action: Record {0} updated.", Id);
            return RedirectToAction("Index");
        }
    }

    [AllowAnonymous]
    public IActionResult Remove(int Id)
    {
        // Dummy action - Record is not actually removed
        // Placeholder code
        TempData["MsgType"] = "danger";
        TempData["Message"] = string.Format("Dummy action: Record {0} removed.", Id);
        return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public IActionResult EditAlias(int Id)
    {
        string select = "SELECT * FROM Warehouse WHERE Id = {0}";
        List<Warehouse> list = DBUtl.GetList<Warehouse>(select, Id);
        if (list.Count == 1)
        {
            return View("Edit", list[0]);
        }
        else
        {
            TempData["Message"] = "Warehouse not found";
            TempData["MsgType"] = "warning";
            return RedirectToAction("Index");
        }
    }

    [AllowAnonymous]
    public IActionResult CheckAlias(string alias)
    {
        string select = $"SELECT * FROM Warehouse WHERE Alias='{alias}'";
        if (DBUtl.GetTable(select).Rows.Count > 0)
        {
            return Json($"[{alias}] already in use");
        }
        return Json(true);
    }
}
