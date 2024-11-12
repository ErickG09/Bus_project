using bus.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bus.Api.Controllers
{
    [ApiController]
    [Route("/api/passengers")]
    public class PassengerController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public PassengerController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _dataContext.Passengers.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var passenger = await _dataContext.Passengers.FirstOrDefaultAsync(x => x.Id == id);
            if (passenger == null)
            {
                return NotFound();
            }
            return Ok(passenger);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Passenger passenger)
        {
            _dataContext.Passengers.Add(passenger);
            await _dataContext.SaveChangesAsync();
            return Ok(passenger);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Passenger passenger)
        {
            _dataContext.Passengers.Update(passenger);
            await _dataContext.SaveChangesAsync();
            return Ok(passenger);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var affectedRows = await _dataContext.Passengers.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
