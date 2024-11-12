using bus.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bus.Api.Controllers
{
    [ApiController]
    [Route("/api/origins")]
    public class OriginController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public OriginController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _dataContext.Origins.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var origin = await _dataContext.Origins.FirstOrDefaultAsync(x => x.Id == id);
            if (origin == null)
            {
                return NotFound();
            }
            return Ok(origin);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Origin origin)
        {
            _dataContext.Origins.Add(origin);
            await _dataContext.SaveChangesAsync();
            return Ok(origin);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Origin origin)
        {
            _dataContext.Origins.Update(origin);
            await _dataContext.SaveChangesAsync();
            return Ok(origin);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var affectedRows = await _dataContext.Origins.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
