using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using University.Models;

namespace University.Controllers
{
    public class HomeController : Controller
    {
        private UniversityManagementContext _ctx;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserDAO _userDAO;

        public HomeController(UniversityManagementContext ctx, IHttpContextAccessor httpContextAccessor, 
            UserDAO userDAO)
        {
            _ctx = ctx;
            _httpContextAccessor = httpContextAccessor;
            _userDAO = userDAO;
        }

        public IActionResult Index()
        {
            // Lấy danh sách bài viết, thông báo và hình ảnh
            var posts = _ctx.Posts.ToList();
            var announcements = _ctx.Announcements.ToList();

            // Truyền dữ liệu đến view
            ViewData["Posts"] = posts;
            ViewData["Announcements"] = announcements;

            return View();
        }
        public IActionResult Post()
        {

            var posts = _ctx.Posts.ToList();
    
                

            // Lấy danh sách tất cả các danh mục
            var categories = _ctx.PostCategories.ToList();
            ViewData["Categories"] = categories;
            ViewData["Posts"] = posts;
            return View();
        }
        public IActionResult Announcements()
        {

            var announcements = _ctx.Announcements.ToList();



            // Lấy danh sách tất cả các danh mục
            var categories = _ctx.AnnouncementCategories.ToList();
            ViewData["Categories"] = categories;
            ViewData["Announcements"] = announcements;
            return View();
        }
        public IActionResult PostsByCategory(int categoryId)
        {
            // Lấy danh sách bài viết thuộc danh mục có categoryId
            var posts = _ctx.Posts
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            // Lấy danh sách tất cả các danh mục
            var categories = _ctx.PostCategories.ToList();

            // Truyền thông tin của các bài viết thuộc danh mục và danh sách danh mục đến view
            ViewData["Posts"] = posts;
            ViewData["Categories"] = categories;

            return View("Post"); // Sử dụng lại view Post.cshtml để hiển thị danh sách bài viết thuộc danh mục
        }
        public IActionResult AnnouncementsByCategory(int categoryId)
        {
            // Lấy danh sách bài viết thuộc danh mục có categoryId
            var announcements = _ctx.Announcements
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            // Lấy danh sách tất cả các danh mục
            var categories = _ctx.AnnouncementCategories.ToList();

            // Truyền thông tin của các bài viết thuộc danh mục và danh sách danh mục đến view
            ViewData["Announcements"] = announcements;
            ViewData["Categories"] = categories;

            return View("Announcements"); // Sử dụng lại view Post.cshtml để hiển thị danh sách bài viết thuộc danh mục
        }
        public IActionResult PostDetail(Int16 id)
        {

            var post = _ctx.Posts
         .Include(p => p.Category)
                .FirstOrDefault(p => p.PostId == id);

            // Lấy danh sách tất cả các danh mục
            var categories = _ctx.PostCategories.ToList();
            ViewData["Categories"] = categories;
            return View(post);
        }
        public IActionResult AnnouceDetail(Int16 id)
        {

            var annouce = _ctx.Announcements
                .Include(p => p.Category)
                .FirstOrDefault(p => p.AnnouncementId == id);

            // Lấy danh sách tất cả các danh mục
            var categories = _ctx.AnnouncementCategories.ToList();
            ViewData["Categories"] = categories;

            return View(annouce);
        }

        public IActionResult TeachingSchedule()
        {
            var teachingSchedules = _ctx.TeachingSchedules
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Subject)
                .Include(ts => ts.Class)
                .ToList();

            return View(teachingSchedules);
        }
        public IActionResult HolidaySchedule()
        {
            var holidaySchedules = _ctx.HolidaySchedules.ToList();
            return View(holidaySchedules);
        }
        public IActionResult MeetingSchedule()
        {
            var meetingSchedules = _ctx.MeetingSchedules.ToList();
            return View(meetingSchedules);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Dang nhap
        [HttpPost]
        public IActionResult Login(User user)
        {
            User existingUser = _userDAO.GetUserByUsername(user.Username);

            if (existingUser == null || existingUser.Password != user.Password)
            {
                ModelState.AddModelError("LoginError", "Invalid username or password");
                return View();
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, existingUser.UserId.ToString())
    };

            if (existingUser.Role == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme))).Wait();
                return RedirectToAction("Index", "Admin");
            }
            if (existingUser.Role == "Teacher")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Teacher"));
                HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme))).Wait();
                return RedirectToAction("Index", "Teacher");
            }
            HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme))).Wait();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {

            HttpContext.SignOutAsync(); // Đăng xuất người dùng

            // Chuyển hướng đến trang chủ 
            return RedirectToAction("Index");
        }

        // Lấy thông tin user ID từ cookie
        private int GetUserId()
        {
            if (_httpContextAccessor.HttpContext.User.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            {

                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
            }

            return 0;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
