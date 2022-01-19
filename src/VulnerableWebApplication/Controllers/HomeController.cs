using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VulnerableWebApplication.Database;
using VulnerableWebApplication.Models;

namespace VulnerableWebApplication.Controllers;

[Route("home")]
public class HomeController : Controller
{
    private readonly ApplicationDbContext context;

    public HomeController(ApplicationDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public IActionResult HomeView()
    {
        string loggedUserId = HttpContext.Session.GetString("loggedUserId");
        string loggedUserName = HttpContext.Session.GetString("loggedUserName");

        if (loggedUserId is null || loggedUserName is null)
        {
            return new RedirectResult("/login");
        }

        ViewBag.Username = loggedUserName;
        return View("Home");
    }

    [HttpPost("password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
    {
        string loggedUserId = HttpContext.Session.GetString("loggedUserId");

        if (loggedUserId is null)
        {
            return Unauthorized();
        }
        
        ApplicationUser user = await this.context
            .ApplicationUsers
            .FindAsync(Guid.Parse(loggedUserId));

        user.Password = dto.NewPassword;

        this.context.ApplicationUsers.Update(user);
        await this.context.SaveChangesAsync();
        
        string loggedUserName = HttpContext.Session.GetString("loggedUserName");

        ViewBag.Username = loggedUserName;
        return View("Home");
    }
}