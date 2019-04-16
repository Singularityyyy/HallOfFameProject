using HallOfFameProject.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HallOfFameProject.Service
{
    public interface IPersonService
    {
        Task<ActionResult<IEnumerable<Person>>> GetAllPeopleAsync();        
        Task<int> SaveChangesAsync();
        Task<Person> FindPersonByIdAsync(long id);

        void AddPerson(Person person);
        void RemovePerson(Person person);
        void SetPersonStateModified(Person person);

        bool PersonExists(long id);
    }
}
