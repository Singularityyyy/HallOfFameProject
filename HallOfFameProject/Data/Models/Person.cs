using Audit.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HallOfFameProject.Data.Models
{
    [AuditIgnore]
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PersonId { get; set; }

        [Required]
        [Display(Name = "Person's name")]
        [StringLength(40, ErrorMessage = "Limit person's name to 40 characters.")]
        public string Name { get; set; }

        [Display(Name = "Person's display name")]
        [StringLength(20, ErrorMessage = "Limit person's display name to 20 characters.")]
        public string DisplayName { get; set; }

        public virtual List<Skill> Skills { get; set; }
    }
}
