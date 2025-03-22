using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Entities
{
    public class Hospital : BaseEntity
    {
        public required string Name { get; set; }

        public required string Phone { get; set; }

        public required string Address { get; set; }

        public required int CityId { get; set; }
        public virtual City City { get; set; }


        [DisplayName("Number Of BME")]
        public required int NumberOfBME { get; set; }

        [DisplayName("Number of Beds")]
        public required int NumberOfBeds { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        // Navigation property for Partners (one-to-many)
        public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();

        // Navigation property for BME Account (one-to-many)
        public virtual ICollection<BMEAccount> BMEAccounts { get; set; } = new List<BMEAccount>();
        // Navigation property to link to departments
        public ICollection<Department> Departments { get; set; }
    }
}
