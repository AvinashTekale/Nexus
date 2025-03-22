using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Entities
{
    [Table("BreakDownClose")]
    public class BreakDownCloseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int BreakDownOpenId { get; set; } // Foreign Key to Open Breakdown Ticket

        [ForeignKey("BreakDownOpenId")]
        public BreakDownOpenEntity BreakDownOpen { get; set; } // Navigation Property

        [Required]
        [MaxLength(500)]
        public string ActionTaken { get; set; } // Actions taken to resolve the issue

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // (Close / Under Observation / Open for Part)

        [Required]
        public bool PartRequired { get; set; } // Yes / No

        // One Breakdown Close can have multiple parts
        public ICollection<PartDetailEntity> PartDetails { get; set; }

        [MaxLength(500)]
        public string FollowUpAction { get; set; } // Any additional actions required
    }
}
