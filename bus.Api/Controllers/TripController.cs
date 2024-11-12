using bus.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bus.Api.Controllers
{
    [ApiController]
    [Route("/api/trips")]
    public class TripController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public TripController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _dataContext.Trips.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var trip = await _dataContext.Trips.FirstOrDefaultAsync(x => x.Id == id);
            if (trip == null)
            {
                return NotFound();
            }
            return Ok(trip);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Trip trip)
        {
            _dataContext.Trips.Add(trip);
            await _dataContext.SaveChangesAsync();
            return Ok(trip);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Trip trip)
        {
            _dataContext.Trips.Update(trip);
            await _dataContext.SaveChangesAsync();
            return Ok(trip);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var affectedRows = await _dataContext.Trips.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
