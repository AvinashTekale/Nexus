using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Entities
{
    [Table("PurchaseOrders")]
    public class PurchaseOrderEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Primary Key

        [Required]
        [MaxLength(50)]
        public string PONumber { get; set; }  // Purchase Order Number

        [Required]
        [MaxLength(100)]
        public string Partner { get; set; }  // Partner associated with the PO

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }  // Total purchase price

        [Required]
        public DateTime PurchaseDate { get; set; }  // Purchase date

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;  // Record creation date

        public DateTime? DateModified { get; set; }  // Record last modification date (nullable)

        [Required]
        public int CreatedBy { get; set; }  // User who created the record

        public int? ModifiedBy { get; set; }  // User who last modified the record (nullable)

        public ICollection<InstallationBaseEntity> InstallationBases { get; set; } = new List<InstallationBaseEntity>();
    }
}