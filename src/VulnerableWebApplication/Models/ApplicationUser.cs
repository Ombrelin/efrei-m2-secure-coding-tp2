namespace VulnerableWebApplication.Models;

public class ApplicationUser
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}