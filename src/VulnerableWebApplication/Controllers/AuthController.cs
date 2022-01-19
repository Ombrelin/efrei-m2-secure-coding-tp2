using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VulnerableWebApplication.Database;
using VulnerableWebApplication.Models;

namespace VulnerableWebApplication.Controllers;

[Route("login")]
public class AuthController : Controller
{
    private readonly ApplicationDbContext context;

    public AuthController(ApplicationDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public IActionResult LoginView()
    {
        return View("Login");
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        ApplicationUser user = await this.context
            .ApplicationUsers
            .FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (user is not null && user.Password == dto.Password)
        {
            HttpContext.Session.SetString("loggedUserId", user.Id.ToString());
            HttpContext.Session.SetString("loggedUserName", user.Username);
            return new RedirectResult("home");
        }

        ViewBag.Error = "Invalid credentials";
        
        return View("Login");
        
    }
}