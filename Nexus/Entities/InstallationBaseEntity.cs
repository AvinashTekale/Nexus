using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Entities
{
    [Table("InstallationBases")]
    public class InstallationBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string SerialNumber { get; set; } // ✅ Select Serial Number

        [Required]
        [MaxLength(100)]
        public string Department { get; set; } // ✅ Department

        [Required]
        [MaxLength(100)]
        public string Location { get; set; } // ✅ Location

        [Required]
        [MaxLength(50)]
        public string Importance { get; set; } // ✅ Importance

        // ✅ PM (Planned Maintenance) - Frequency & Count
        [Required]
        public int PMFrequency { get; set; }

        [Required]
        public int PMCount { get; set; }

        // ✅ CM (Corrective Maintenance) - Frequency & Count
        [Required]
        public int CMFrequency { get; set; }

        [Required]
        public int CMCount { get; set; }

        [Required]
        public DateTime InstallationDate { get; set; } // ✅ Installation Date

        // ✅ Foreign Key for Purchase Order Relationship
        [Required]
        public int PurchaseOrderId { get; set; }

        // ✅ Navigation Property
        [ForeignKey("PurchaseOrderId")]
        public PurchaseOrderEntity PurchaseOrder { get; set; }
    }
}
