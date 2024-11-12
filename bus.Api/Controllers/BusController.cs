using bus.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bus.Api.Controllers
{
    [ApiController]
    [Route("/api/buses")]
    public class BusController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public BusController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _dataContext.Buses.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var bus = await _dataContext.Buses.FirstOrDefaultAsync(x => x.Id == id);
            if (bus == null)
            {
                return NotFound();
            }
            return Ok(bus);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Bus bus)
        {
            _dataContext.Buses.Add(bus);
            await _dataContext.SaveChangesAsync();
            return Ok(bus);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Bus bus)
        {
            _dataContext.Buses.Update(bus);
            await _dataContext.SaveChangesAsync();
            return Ok(bus);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var affectedRows = await _dataContext.Buses.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
