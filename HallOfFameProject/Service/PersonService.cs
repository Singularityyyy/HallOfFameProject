using HallOfFameProject.Data;
using HallOfFameProject.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace HallOfFameProject.Service
{
    public class PersonService : IPersonService
    {
        private readonly HallOfFameDbContext _context;
        
        public PersonService(HallOfFameDbContext context)
        {
            _context = context;
        }
       
        public async Task<ActionResult<IEnumerable<Person>>> GetAllPeopleAsync()
        {
            return await _context.People.ToListAsync();
        }

        public IEnumerable<Person> GetAllPeople()
        {
            return _context.People;
        }

        public IEnumerable<Skill> GetAllPersonSkills(Person person)
        {
            return person.Skills;
        }

        public bool PersonExists(long id)
        {
            return _context.People.Any(e => e.PersonId == id);
        }
        
        public void SetPersonStateModified(Person person)
        {
            if (person != null)
            {
                _context.Entry(person).State = EntityState.Modified;
            }
        }    
         
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        
        public void AddPerson(Person person)
        {
            _context.People.Add(person);
            return;
        }

        public async Task<Person> FindPersonByIdAsync(long id)
        {
            return await _context.People.FindAsync(id);
        }

        public void RemovePerson(Person person)
        {
            _context.People.Remove(person);
        }
    }
}
