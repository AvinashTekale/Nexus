using Microsoft.AspNetCore.Mvc;

namespace Nexus.Controllers
{
    public class BaseAdminController : Controller
    {
        public bool IsAdminUser()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                return false;
            }

            string userRole = HttpContext.Session.GetString("UserRole");

            if (userRole != null && userRole == "Admin")
            {
                return true;
            }
            else
            {
                return false;

            }

        }

    }
}
