using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Model;

namespace PeopleManager.Services
{
    public class PersonService
    {
        private readonly PeopleManagerDbContext _dbContext;

        public PersonService(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public async Task<IList<PersonResult>> FindAsync()
        {
            return await _dbContext.People
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .Select(p => new PersonResult
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    Description = p.Description,
                    NumberOfResponsibleVehicles = p.ResponsibleForVehicles.Count
                })
                .ToListAsync();
        }

        //Get by id
        public async Task<PersonResult?> GetAsync(int id)
        {
            return await _dbContext.People
                .Select(p => new PersonResult
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    Description = p.Description,
                    NumberOfResponsibleVehicles = p.ResponsibleForVehicles.Count
                })
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //Create
        public async Task<PersonResult?> CreateAsync(PersonRequest request)
        {
            var person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Description = request.Description
            };
            _dbContext.Add(person);

            await _dbContext.SaveChangesAsync();

            return await GetAsync(person.Id);
        }

        //Update
        public async Task<PersonResult?> UpdateAsync(int id, PersonRequest person)
        {
            var dbPerson = await _dbContext.People.FindAsync(id);
            if (dbPerson is null)
            {
                return null;
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;
            dbPerson.Description = person.Description;

            await _dbContext.SaveChangesAsync();

            return await GetAsync(id);
        }

        //Delete
        public async Task DeleteAsync(int id)
        {
            var person = new Person
            {
                Id = id,
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty
            };
            _dbContext.People.Attach(person);

            _dbContext.People.Remove(person);

            await _dbContext.SaveChangesAsync();
        }
    }
}
