using Nexus.Entities;
using System.ComponentModel;

namespace Nexus.DTOs
{
    public class HospitalDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string Phone { get; set; }

        public required string Address { get; set; }

        public required int CityId { get; set; }
        public required int StateId { get; set; }

        [DisplayName("Number Of BME")]
        public required int NumberOfBME { get; set; }

        [DisplayName("Number of Beds")]
        public required int NumberOfBeds { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public string Role { get; set; } = "Hospital";
    }
}
