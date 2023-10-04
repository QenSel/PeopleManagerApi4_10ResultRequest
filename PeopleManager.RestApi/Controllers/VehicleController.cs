
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Dto.Requests;
using PeopleManager.Model;
using PeopleManager.Services;

namespace PeopleManager.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleService _vehicleService;

        public VehiclesController(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAsync()
        {
            var vehicles = await _vehicleService.FindAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id:int}", Name = "GetVehicleRoute")]
        public async Task<IActionResult> GetAsync([FromRoute]int id)
        {
            var vehicles = await _vehicleService.GetAsync(id);
            return Ok(vehicles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]VehicleRequest model)
        {
            var vehicle = await _vehicleService.CreateAsync(model);
            if (vehicle is null)
            {
                return NotFound();
            }
            return CreatedAtRoute("GetVehicleRoute", new {id = vehicle.Id}, vehicle);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditAsync([FromRoute]int id, [FromBody]VehicleRequest model)
        {
            var vehicle = await _vehicleService.UpdateAsync(id, model);
            if (vehicle is null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
            await _vehicleService.DeleteAsync(id);

            return Ok();
        }
    }
}
