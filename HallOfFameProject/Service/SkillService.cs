using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HallOfFameProject.Data;
using HallOfFameProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HallOfFameProject.Service
{
    public class SkillService : ISkillService
    {
        private readonly HallOfFameDbContext _context;
        public SkillService(HallOfFameDbContext context)
        {
            _context = context;
        }
        public void CascadeDeleteSkills(Person person)
        {
            foreach (var s in person.Skills)
            {
                _context.Skills.Remove(s);
            }
        }

        public void NullifySkillId(Person person)
        {
            //Since all IDs are set by Database, I had to nullify the IDs provided  by Swagger user in Swagger body. This does not affect anything.
            foreach (var s in person.Skills)
            {
                s.SkillId = 0;
            }

        }        

        public void SetSkillsModified(Person person)
        {
            if (person.Skills != null)
            {
                foreach (var s in person.Skills)
                {
                    _context.Entry(s).State = EntityState.Modified;
                }
            }
        }
    }
}
