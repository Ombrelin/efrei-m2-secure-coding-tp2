using Microsoft.AspNetCore.Mvc;


namespace AttackerWebSite.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View("Index");
    }
}