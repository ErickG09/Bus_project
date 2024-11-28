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
            var trips = await _dataContext.Trips
                .Include(t => t.Driver) // Incluye la relación con el conductor
                .Include(t => t.Bus)    // Incluye la relación con el autobús
                .ThenInclude(b => b.Company) // Incluye la relación del autobús con la compañía
                .Select(t => new
                {
                    t.Id,
                    t.Price,
                    t.Schedule,
                    t.DepartureDate,
                    t.ArrivalDate,
                    t.Platform,
                    Driver = t.Driver != null ? new
                    {
                        t.Driver.Id,
                        t.Driver.Name,
                        t.Driver.License
                    } : null,
                    Bus = t.Bus != null ? new
                    {
                        t.Bus.Id,
                        t.Bus.Seats,
                        t.Bus.Category,
                        Company = t.Bus.Company != null ? new
                        {
                            t.Bus.Company.Id,
                            t.Bus.Company.CompanyName
                        } : null
                    } : null
                })
                .ToListAsync();

            return Ok(trips);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var trip = await _dataContext.Trips
                .Include(t => t.Driver) // Incluye la relación con el conductor
                .Include(t => t.Bus)    // Incluye la relación con el autobús
                .ThenInclude(b => b.Company) // Incluye la relación del autobús con la compañía
                .Where(t => t.Id == id)
                .Select(t => new
                {
                    t.Id,
                    t.Price,
                    t.Schedule,
                    t.DepartureDate,
                    t.ArrivalDate,
                    t.Platform,
                    Driver = t.Driver != null ? new
                    {
                        t.Driver.Id,
                        t.Driver.Name,
                        t.Driver.License
                    } : null,
                    Bus = t.Bus != null ? new
                    {
                        t.Bus.Id,
                        t.Bus.Seats,
                        t.Bus.Category,
                        Company = t.Bus.Company != null ? new
                        {
                            t.Bus.Company.Id,
                            t.Bus.Company.CompanyName
                        } : null
                    } : null
                })
                .FirstOrDefaultAsync();

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
