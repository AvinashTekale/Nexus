using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus.Data;

namespace Nexus.Controllers
{
    public class AdminDashboardController : BaseAdminController
    {
        private readonly NexusDBContext _context;

        public AdminDashboardController(NexusDBContext nexusDBContext)
        {
            _context = nexusDBContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var user = IsAdminUser();
            if (string.IsNullOrEmpty(userEmail) && user)
            {
                return RedirectToAction("Login", "Auth");
            }

            var admin = _context.Users.FirstOrDefault(a => a.Email == userEmail);

            return View(admin); // Pass the admin data to the view
        }
    }
}
