using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace University.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AdminController : Controller
    {
        private UniversityManagementContext _ctx;
        private UserDAO _userDAO;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminController(
            UniversityManagementContext ctx, UserDAO userDAO, IHttpContextAccessor httpContextAccessor
            )
        {

            _ctx = ctx;
            _userDAO = userDAO;
            _httpContextAccessor = httpContextAccessor;


        }
        
       
        [HttpGet]
        public IActionResult Logout()
        {

            HttpContext.SignOutAsync(); // Đăng xuất người dùng

            // Chuyển hướng đến trang chủ 
            return RedirectToAction("Index","Home");
        }
        public int GetUserId()
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

       
        public IActionResult Index()
        {
            return View();
        }
     


   
    }

}

