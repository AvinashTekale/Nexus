namespace Nexus.Entities
{
    public class User: BaseEntity
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public required byte[] PasswordHash { get; set; }

        public required byte[] PasswordKey { get; set; }
        public required string Role { get; set; } = "Admin";
    }
}
