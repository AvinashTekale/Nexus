namespace Nexus.DTOs
{
    public class RegisterDTO
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }
        public string Role { get; set; } = "Admin";
    }
    public class LoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

}
