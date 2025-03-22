using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Entities
{
    [Table("ContractOrders")]
    public class ContractOrderEntity
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
        public string ContractNumber { get; set; } // Contract Order Number

        [Required]
        public decimal ContractPrice { get; set; } // Updated Price

        [Required]
        public DateTime StartDate { get; set; } // Contract Start Date

        [Required]
        public DateTime EndDate { get; set; } // Contract End Date
    }
}
