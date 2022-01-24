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
        string? loggedUserId = Request.Cookies["loggedUserId"];
        string? loggedUserName = Request.Cookies["loggedUserName"];

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
        string? loggedUserId = Request.Cookies["loggedUserId"];
        string? loggedUserName = Request.Cookies["loggedUserName"];
        
        if (loggedUserId is null || loggedUserName is null)
        {
            return Unauthorized("Not logged in !");
        }
        
        ApplicationUser? user = await this.context
            .ApplicationUsers
            .FindAsync(Guid.Parse(loggedUserId));

        if (user is null)
        {
            return Unauthorized("Not logged in !");
        }
        
        user.Password = dto.NewPassword;

        this.context.ApplicationUsers.Update(user);
        await this.context.SaveChangesAsync();

        ViewBag.Username = loggedUserName;
        return View("Home");
    }
}