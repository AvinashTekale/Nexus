using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Entities
{
    [Table("BreakDownOpen")]
    public class BreakDownOpenEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PurchaseOrderId { get; set; } // Foreign Key to Purchase Order

        [ForeignKey("PurchaseOrderId")]
        public PurchaseOrderEntity PurchaseOrder { get; set; } // Navigation Property

        [Required]
        [MaxLength(50)]
        public string SerialNumber { get; set; } // Selected Serial Number

        [Required]
        [MaxLength(500)]
        public string ProblemReported { get; set; } // Issue description

        [Required]
        public bool AnyPatientHarm { get; set; } // Yes / No

        [MaxLength(500)]
        public string PatientHarmDescription { get; set; } // Description if patient harm occurred

        [Required]
        [MaxLength(100)]
        public string MachineStatus { get; set; } // (System Down / Restrictive Use / Problem Intermittent)

        [Required]
        public DateTime Deadline { get; set; } // Deadline for resolving the issue
    }
}
