using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Model;

namespace PeopleManager.Services
{
    public class VehicleService
    {
        private readonly PeopleManagerDbContext _dbContext;

        public VehicleService(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public async Task<IList<VehicleResult>> FindAsync()
        {
            return await _dbContext.Vehicles
                //.Include(v => v.ResponsiblePerson)
                .ToListAsync();
        }

        //Get by id
        public async Task<VehicleResult?> GetAsync(int id)
        {
            return await _dbContext.Vehicles
                //.Include(v => v.ResponsiblePerson)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        //Create
        public async Task<VehicleResult?> CreateAsync(VehicleRequest vehicle)
        {
            _dbContext.Add(vehicle);
            await _dbContext.SaveChangesAsync();

            return vehicle;
        }

        //Update
        public async Task<VehicleResult?> UpdateAsync(int id, VehicleRequest vehicle)
        {
            var dbVehicle = await _dbContext.Vehicles.FindAsync(id);
            if (dbVehicle is null)
            {
                return null;
            }

            dbVehicle.LicensePlate = vehicle.LicensePlate;
            dbVehicle.Brand = vehicle.Brand;
            dbVehicle.Type = vehicle.Type;
            dbVehicle.ResponsiblePersonId = vehicle.ResponsiblePersonId;

            await _dbContext.SaveChangesAsync();

            return dbVehicle;
        }

        //Delete
        public async Task DeleteAsync(int id)
        {
            var vehicle = new Vehicle
            {
                Id = id,
                LicensePlate = string.Empty
            };
            _dbContext.Vehicles.Attach(vehicle);

            _dbContext.Vehicles.Remove(vehicle);

            await _dbContext.SaveChangesAsync();
        }
    }
}
