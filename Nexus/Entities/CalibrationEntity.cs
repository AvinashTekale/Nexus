using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Entities
{
    [Table("Calibration")]
    public class CalibrationEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PurchaseOrderId { get; set; } // Foreign Key to Purchase Order

        [ForeignKey("PurchaseOrderId")]
        public PurchaseOrderEntity PurchaseOrder { get; set; } // Navigation Property

        [Required]
        [MaxLength(500)]
        public string ActionTaken { get; set; } // Free Text for Service Action

        public string ServiceReportPath { get; set; } // File Path for Attachment

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } // (Open / Closed)

        [Required]
        public DateTime NextCalibrationDate { get; set; } // Next Due Date for Calibration
    }
}
