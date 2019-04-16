using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HallOfFameProject.Data.Models;
using HallOfFameProject.Service;

namespace HallOfFameProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
       
        private readonly IPersonService _personService;
        private readonly ISkillService _skillService;

        public PeopleController(IPersonService personService, ISkillService skillService)
        {            
            _personService = personService;
            _skillService = skillService;
        }

        //GET: api/People
       [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {            
            return await _personService.GetAllPeopleAsync();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(long id)
        {
            var person = await _personService.FindPersonByIdAsync(id);

            if (person == null)
            {
                return NotFound("No such person by id ---" + id + "---");
            }

            return person;
        }

        // PUT: api/People/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson([FromRoute]long id, [FromBody]Person person)
        {
            person.PersonId = id;

            _personService.SetPersonStateModified(person);
            _skillService.SetSkillsModified(person);
                        
            try
            {
                await _personService.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_personService.PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/People
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            //Since all IDs are set by the Database, I had to nullify the IDs provided by Swagger user in SwaggerUI request body. This does not affect anything [I guess so].
            //Another way was to create a special model of "Person" [for the request body] that would be lacking the PersonId field.
            person.PersonId = 0;
            _skillService.NullifySkillId(person);

            _personService.AddPerson(person);
            await _personService.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.PersonId }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(long id)
        {
            var person = await _personService.FindPersonByIdAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            _skillService.CascadeDeleteSkills(person);
            _personService.RemovePerson(person);

            await _personService.SaveChangesAsync();

            return person;
        }
    }
}
