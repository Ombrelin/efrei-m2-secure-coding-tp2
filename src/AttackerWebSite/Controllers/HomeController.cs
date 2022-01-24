using Microsoft.AspNetCore.Mvc;


namespace AttackerWebSite.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Target = Environment.GetEnvironmentVariable("TARGET_URL") ?? throw new ArgumentException("TARGET_URL is not populated");
        return View("Index");
    }
}