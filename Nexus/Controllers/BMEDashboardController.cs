using Microsoft.AspNetCore.Mvc;
using Nexus.Data;

namespace Nexus.Controllers
{
    public class BMEDashboardController : Controller
    {
        private readonly NexusDBContext _context;

        public BMEDashboardController(NexusDBContext nexusDBContext)
        {
            _context = nexusDBContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var role = HttpContext.Session.GetString("UserRole");
            if (string.IsNullOrEmpty(userEmail) && role != "BME")
            {
                return RedirectToAction("Login", "Auth");
            }

            var user = _context.Users.FirstOrDefault(a => a.Email == userEmail);

            return View(user); // Pass the user data to the view
        }
    }
}
