using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Nexus.Entities
{
    public class Department:BaseEntity
    {
        public required string Name { get; set; }
        public required string Location { get; set; }

        // Foreign Key for Hospital
        public int HospitalId { get; set; }

        // Navigation property to link back to Hospital
        [ValidateNever]
        public Hospital Hospital { get; set; }
    }
}
