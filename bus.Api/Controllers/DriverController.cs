using bus.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace bus.Api.Controllers
{
    [ApiController]
    [Route("/api/drivers")]
    public class DriverController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public DriverController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _dataContext.Drivers.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var driver = await _dataContext.Drivers.FirstOrDefaultAsync(x => x.Id == id);
            if (driver == null)
            {
                return NotFound();
            }
            return Ok(driver);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Driver driver)
        {
            _dataContext.Drivers.Add(driver);
            await _dataContext.SaveChangesAsync();
            return Ok(driver);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Driver driver)
        {
            _dataContext.Drivers.Update(driver);
            await _dataContext.SaveChangesAsync();
            return Ok(driver);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var affectedRows = await _dataContext.Drivers.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
