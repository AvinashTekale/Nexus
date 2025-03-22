using Microsoft.AspNetCore.Mvc;
using Nexus.Data;

namespace Nexus.Controllers
{
    public class DashboardController : Controller
    {
        private readonly NexusDBContext _context;

        public DashboardController(NexusDBContext nexusDBContext)
        {
            _context = nexusDBContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var user = HttpContext.Session.GetString("UserRole");
            if (string.IsNullOrEmpty(userEmail) && user != "Hospital")
            {
                return RedirectToAction("Login", "Auth");
            }

            var admin = _context.Users.FirstOrDefault(a => a.Email == userEmail);

            return View(admin); // Pass the admin data to the view
        }
    }
}
