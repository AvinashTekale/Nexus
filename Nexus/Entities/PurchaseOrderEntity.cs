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
        [MaxLength(50)]
        public string SerialNumber { get; set; }  // Unique Serial Number

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }  // Select or enter manually

        [Required]
        [MaxLength(100)]
        public string Category { get; set; }  // Select or enter manually

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }  // Individual price

        [Required]
        [MaxLength(100)]
        public string ServiceContact { get; set; }  // Service contact person

        [Required]
        [MaxLength(100)]
        public string SalesContact { get; set; }  // Sales contact person

        [Required]
        public DateTime WarrantyStartDate { get; set; }  // Warranty start date

        [Required]
        public DateTime WarrantyEndDate { get; set; }  // Warranty end date

        public ICollection<InstallationBaseEntity> InstallationBases { get; set; } = new List<InstallationBaseEntity>();
    }
}
