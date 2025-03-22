namespace Nexus.DTOs
{
    public class BMEAccountDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string Phone { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public int HospitalId { get; set; }

        public string Role { get; set; } = "BME";
    }
}
