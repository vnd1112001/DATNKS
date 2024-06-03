using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using University.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.Name = "YourCookieName";
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/home/login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Thời gian hết hạn của cookie

});


builder.Services.AddAuthorization();
builder.Services.AddSession(); 
builder.Services.AddScoped<UserDAO>();
//Dependency Injection
builder.Services.AddDbContext<UniversityManagementContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityDB"));
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapAreaControllerRoute(
    name: "MyAreas2",
    areaName: "Admin",
    pattern: "Admin/{action=Index}/{id?}",
    defaults: new { controller = "Admin", action = "Index" });
app.MapAreaControllerRoute(
    name: "MyAreas2",
    areaName: "Admin",
    pattern: "Admin/Users/{action=Index}/{id?}",
    defaults: new { controller = "Users", action = "Index" });
app.MapAreaControllerRoute(
    name: "MyAreas2",
    areaName: "Admin",
    pattern: "Admin/Classes/{action=Index}/{id?}",
    defaults: new { controller = "Classes", action = "Index" });
app.MapAreaControllerRoute(
    name: "MyAreas2",
    areaName: "Admin",
    pattern: "Admin/Subjects/{action=Index}/{id?}",
    defaults: new { controller = "Subjects", action = "Index" });
app.MapAreaControllerRoute(
    name: "MyAreas2",
    areaName: "Admin",
    pattern: "Admin/HolidaySchedules/{action=Index}/{id?}",
    defaults: new { controller = "HolidaySchedules", action = "Index" });
app.MapAreaControllerRoute(
    name: "MyAreas2",
    areaName: "Admin",
    pattern: "Admin/TeachingSchedules/{action=Index}/{id?}",
    defaults: new { controller = "TeachingSchedules", action = "Index" });
app.MapAreaControllerRoute(
    name: "MyAreas2",
    areaName: "Admin",
    pattern: "Admin/MeetingSchedules/{action=Index}/{id?}",
    defaults: new { controller = "MeetingSchedules", action = "Index" });
app.MapAreaControllerRoute(
    name: "MyAreas",
    areaName: "Teacher",
    pattern: "Teacher/{action=Index}/{id?}",
    defaults: new { controller = "Teacher", action = "Index" });
app.MapAreaControllerRoute(
    name: "MyAreas",
    areaName: "Teacher",
    pattern: "Teacher/AnnouncementCategories/{action=Index}/{id?}",
    defaults: new { controller = "AnnouncementCategories", action = "Index" });
app.MapAreaControllerRoute(
    name: "MyAreas",
    areaName: "Teacher",
    pattern: "Teacher/PostCategories/{action=Index}/{id?}",
    defaults: new { controller = "PostCategories", action = "Index" });
app.MapAreaControllerRoute(
    name: "MyAreas",
    areaName: "Teacher",
    pattern: "Teacher/Announcements/{action=Index}/{id?}",
    defaults: new { controller = "Announcements", action = "Index" });
app.MapAreaControllerRoute(
    name: "MyAreas",
    areaName: "Teacher",
    pattern: "Teacher/Posts/{action=Index}/{id?}",
    defaults: new { controller = "Posts", action = "Index" });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
