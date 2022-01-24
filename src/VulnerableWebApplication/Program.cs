using Microsoft.EntityFrameworkCore;
using Npgsql;
using VulnerableWebApplication.Database;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddDistributedMemoryCache();
// builder.Services.AddSession(options =>
// {
//     options.IdleTimeout = TimeSpan.FromSeconds(120);
//     options.Cookie.HttpOnly = true;
//     options.Cookie.IsEssential = true;
//     options.Cookie.SameSite = SameSiteMode.None;
// });
// builder.Services.AddAntiforgery(options =>
// {
//     options.SuppressXFrameOptionsHeader = true;
// });


// Database
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    string databaseHost = Environment.GetEnvironmentVariable("DATABASE_HOST");
    string databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");
    string databaseUsername = Environment.GetEnvironmentVariable("DATABASE_USERNAME");
    string databasePassword = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");

    var builder = new NpgsqlConnectionStringBuilder
    {
        Host = databaseHost,
        Port = 5432,
        Username = databaseUsername,
        Password = databasePassword,
        Database = databaseName
    };
    
    options.UseNpgsql(builder.ToString());
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using var serviceScope = app.Services.CreateScope();
serviceScope
    .ServiceProvider
    .GetService<ApplicationDbContext>()
    .Database
    .Migrate();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());


app.UseAuthorization();
//app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();