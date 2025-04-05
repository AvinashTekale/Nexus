using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Nexus.Entities
{
    public class EquipmentModel : BaseEntity
    {


        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name length can't exceed 100 characters.")]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Partner")]
        public int PartnerId { get; set; }

        // Navigation property for the Partner
        [ValidateNever]
        public virtual Partner Partner { get; set; }
    }
}
