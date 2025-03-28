using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Entities
{
    public class InstallationBaseEntity
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        public int PurchaseOrderId { get; set; } // Foreign Key to Purchase Table

        [ForeignKey("PurchaseOrderId")]
        public virtual PurchaseOrderEntity PurchaseOrder { get; set; }

        [Required]
        public int EquipmentId { get; set; } // Foreign Key to Equipment Table

        [ForeignKey("EquipmentId")]
        public virtual Equipment Equipment { get; set; }

        [Required]
        [MaxLength(50)]
        public string SerialNumber { get; set; } // Fetch from Equipment and store

        [Required]
        public int DepartmentId { get; set; } // Foreign Key to Department Table

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        [Required]
        [MaxLength(255)]
        public string Location { get; set; } // Location of Equipment

        [Required]
        public string Importance { get; set; } // Critical / Non-Critical

        // Preventive Maintenance (PM)
        [Required]
        public int PMFrequency { get; set; } // Frequency of Preventive Maintenance

        [Required]
        public int PMCount { get; set; } // Number of PMs conducted

        // Corrective Maintenance (CM)
        [Required]
        public int CMFrequency { get; set; } // Frequency of Corrective Maintenance

        [Required]
        public int CMCount { get; set; } // Number of CMs conducted

        [Required]
        public DateTime InstallationDate { get; set; } // Equipment Installation Date

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; } = DateTime.UtcNow;

        [Required]
        public string CreatedBy { get; set; } // User who created

        public string ModifiedBy { get; set; } // User who last modified
    }
}
