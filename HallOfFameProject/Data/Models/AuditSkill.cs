using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HallOfFameProject.Data.Models
{
    public class AuditSkill 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AuditId { get; set; }
        public DateTime AuditDate { get; set; }
        public string AuditAction { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }
        public long SkillId { get; set; }
    }
}
