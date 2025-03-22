using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Entities
{
    public class EquipmentCategory : BaseEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name length can't exceed 100 characters.")]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Hospital")]
        public int HospitalId { get; set; }

        // Navigation property (optional)
        [ValidateNever]
        public virtual Hospital Hospital { get; set; }
    }
}
