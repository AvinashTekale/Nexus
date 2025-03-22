namespace Nexus.DTOs
{
    public class ContactPersonDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string Phone { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public required int PartnerId { get; set; }

        public required string Role { get; set; }
    }
}
