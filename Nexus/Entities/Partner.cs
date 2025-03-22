using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Entities
{
    public class Partner : BaseEntity
    {
        public required string Name { get; set; }

        public required string Location { get; set; }

        // Multi-select Type (e.g., Manufacturer, Vendor, Service Provider)
        public required string Types { get; set; } // Comma-separated values

        // Foreign key for Hospital
        public required int HospitalId { get; set; }

        // Navigation property for Hospital
        [ValidateNever]
        public virtual Hospital Hospital { get; set; }
        // Navigation property to link to departments
        public ICollection<ContactPerson> ContactPersons { get; set; }
    }


}
