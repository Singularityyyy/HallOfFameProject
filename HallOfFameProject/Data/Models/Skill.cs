using Audit.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HallOfFameProject.Data.Models
{
    [AuditInclude]
    public class Skill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SkillId { get; set; }

        [Required]
        [Display(Name = "Skill name")]
        [StringLength(20, ErrorMessage = "Limit skill name to 20 characters.")]
        public string Name { get; set; }

        [Display(Name = "Level of competence")]
        [Range(1, 10, ErrorMessage = "Skill level is measured by a ten-point scale.")]
        public byte Level { get; set; }

        //public virtual List<SkillHistory> HistoryRecords { get; set; }
    }
}
