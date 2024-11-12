using bus.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bus.Api.Controllers
{
    [ApiController]
    [Route("/api/tripdetails")]
    public class TripDetailController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public TripDetailController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _dataContext.TripDetails.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var tripDetail = await _dataContext.TripDetails.FirstOrDefaultAsync(x => x.Id == id);
            if (tripDetail == null)
            {
                return NotFound();
            }
            return Ok(tripDetail);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(TripDetail tripDetail)
        {
            _dataContext.TripDetails.Add(tripDetail);
            await _dataContext.SaveChangesAsync();
            return Ok(tripDetail);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(TripDetail tripDetail)
        {
            _dataContext.TripDetails.Update(tripDetail);
            await _dataContext.SaveChangesAsync();
            return Ok(tripDetail);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var affectedRows = await _dataContext.TripDetails.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
