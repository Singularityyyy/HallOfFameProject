using HallOfFameProject.Data.Models;

namespace HallOfFameProject.Service
{
    public interface ISkillService
    {
        void CascadeDeleteSkills(Person person);
        void SetSkillsModified(Person person);
        void NullifySkillId(Person person);
        
    }
}
