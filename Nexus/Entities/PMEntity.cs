using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Entities
{
    [Table("PM")]
    public class PMEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PurchaseOrderId { get; set; } // Foreign Key to Purchase Order

        [ForeignKey("PurchaseOrderId")]
        public PurchaseOrderEntity PurchaseOrder { get; set; } // Navigation Property

        [Required]
        public int Frequency { get; set; } // How often PM is performed

        [Required]
        public int Count { get; set; } // Number of PM cycles

        [Required]
        public DateTime NextPMDate { get; set; } // Next Scheduled PM Date

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } // (Pending, Completed, Overdue)
    }
}
