using System.Net;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Dto.Requests;
using PeopleManager.Model;
using PeopleManager.Services;

namespace PeopleManager.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PersonService _personService;

        public PeopleController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAsync()
        {
            var people = await _personService.FindAsync();
            return Ok(people);
        }

        [HttpGet("{id:int}", Name = "GetPersonRoute")]
        public async Task<IActionResult> GetAsync([FromRoute]int id)
        {
            var people = await _personService.GetAsync(id);
            return Ok(people);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]PersonRequest model)
        {
            var person = await _personService.CreateAsync(model);
            if (person is null)
            {
                return NotFound();
            }
            return CreatedAtRoute("GetPersonRoute", new {id = person.Id}, person);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditAsync([FromRoute]int id, [FromBody]PersonRequest model)
        {
            var person = await _personService.UpdateAsync(id, model);
            if (person is null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
            await _personService.DeleteAsync(id);

            return Ok();
        }
    }
}
