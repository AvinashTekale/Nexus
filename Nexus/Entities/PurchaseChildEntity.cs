using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Entities
{
    public class PurchaseChildEntity
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        public int PurchaseId { get; set; } // Foreign Key to Purchase Table

        [ForeignKey("PurchaseId")]
        public virtual PurchaseOrderEntity PurchaseOrder { get; set; }

        [Required]
        [MaxLength(50)]
        public string SerialNumber { get; set; } // Unique Serial Number

        [Required]
        public int ModelId { get; set; } // Foreign Key to Model Table

        [ForeignKey("ModelId")]
        public virtual EquipmentModel Model { get; set; }

        [Required]
        public int CategoryId { get; set; } // Foreign Key to Category Table

        [ForeignKey("CategoryId")]
        public virtual EquipmentCategory Category { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Individual Price

        [Required]
        public int ServiceContactId { get; set; } // Foreign Key to Service Contact

        [ForeignKey("ServiceContactId")]
        public virtual ContactPerson ServiceContact { get; set; }

        [Required]
        public int SalesContactId { get; set; } // Foreign Key to Sales Contact

        [ForeignKey("SalesContactId")]
        public virtual ContactPerson SalesContact { get; set; }

        public DateTime WarrantyStart { get; set; }
        public DateTime WarrantyEnd { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; } = DateTime.UtcNow;

        [Required]
        public string CreatedBy { get; set; } // User who created

        public string ModifiedBy { get; set; } // User who last modified
    }
}
