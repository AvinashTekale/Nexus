using Microsoft.AspNetCore.Http;

namespace Nexus.Interfaces
{
    public interface IAuthRepository
    {
        bool IsAuthorized(); // Method definition
    }
}
