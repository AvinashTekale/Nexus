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
        public int Id { get; set; } // Primary Key

        // Foreign Key to Purchase Order
        [Required]
        public int PurchaseOrderId { get; set; }

        [ForeignKey("PurchaseOrderId")]
        public virtual PurchaseOrderEntity PurchaseOrder { get; set; }

        // Foreign Key to Equipment (Related to Purchased Equipment)
        [Required]
        public int EquipmentId { get; set; }

        [ForeignKey("EquipmentId")]
        public virtual Equipment Equipment { get; set; }

        // Foreign Key to Department (Where contract applies)
        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        [Required]
        [MaxLength(50)]
        public string ContractNumber { get; set; } // Contract Order Number

        [Required]
        public decimal ContractPrice { get; set; } // Updated Contract Price

        [Required]
        public DateTime StartDate { get; set; } // Contract Start Date

        [Required]
        public DateTime EndDate { get; set; } // Contract End Date

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; } = DateTime.UtcNow;

        [Required]
        public string CreatedBy { get; set; } // User who created

        public string ModifiedBy { get; set; } // User who last modified
    }
}
