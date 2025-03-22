using Microsoft.AspNetCore.Http;
using Nexus.Interfaces;

namespace Nexus.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthorized()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null) return false;

            var userEmail = session.GetString("UserEmail");
            var userRole = session.GetString("UserRole");

            return !string.IsNullOrEmpty(userEmail) && (userRole == "Hospital" || userRole == "BME");
        }
    }
}
