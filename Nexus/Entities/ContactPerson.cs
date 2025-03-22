using System.ComponentModel.DataAnnotations;

namespace Nexus.Entities
{
    public class ContactPerson : BaseEntity
    {

        public required string Phone { get; set; }
        
        // Foreign key for User
        [Required]
        public int UserId { get; set; }

        // Navigation property for User
        public virtual User User { get; set; }

        // Foreign key for Partner
        [Required]
        public int PartnerId { get; set; }

        // Navigation property for Partner
        public virtual Partner Partner { get; set; }
    }
}
