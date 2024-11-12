using bus.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bus.Api.Controllers
{
    [ApiController]
    [Route("/api/destinations")]
    public class DestinationController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public DestinationController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _dataContext.Destinations.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var destination = await _dataContext.Destinations.FirstOrDefaultAsync(x => x.Id == id);
            if (destination == null)
            {
                return NotFound();
            }
            return Ok(destination);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Destination destination)
        {
            _dataContext.Destinations.Add(destination);
            await _dataContext.SaveChangesAsync();
            return Ok(destination);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Destination destination)
        {
            _dataContext.Destinations.Update(destination);
            await _dataContext.SaveChangesAsync();
            return Ok(destination);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var affectedRows = await _dataContext.Destinations.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
