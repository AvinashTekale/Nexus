using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Nexus.Entities
{
    public class EquipmentMaintenance : BaseEntity
    {
        [Required]
        public int EquipmentId { get; set; }

        [ForeignKey("EquipmentId")]
        [ValidateNever]
        public virtual Equipment Equipment { get; set; }

        public int BMEAccountId { get; set; }

        [ForeignKey("BMEAccountId")]
        [ValidateNever]
        public virtual BMEAccount BMEAccount { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public string Status {  get; set; }

        [Required]
        public DateTime MaintenanceDate { get; set; }

        [Required]
        public string MaintenanceDetails { get; set; }

    }
}
