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

        // Obtener todos los buses con la información de la compañía
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var buses = await _dataContext.Buses
                .Include(b => b.Company) // Incluye la compañía relacionada
                .Select(b => new
                {
                    b.Id,
                    b.Seats,
                    b.Category,
                    Company = new
                    {
                        b.Company.Id,
                        b.Company.CompanyName
                    }
                })
                .ToListAsync();

            return Ok(buses);
        }

        // Obtener un bus específico con la información de la compañía
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var bus = await _dataContext.Buses
                .Include(b => b.Company) // Incluye la compañía relacionada
                .Where(b => b.Id == id)
                .Select(b => new
                {
                    b.Id,
                    b.Seats,
                    b.Category,
                    Company = new
                    {
                        b.Company.Id,
                        b.Company.CompanyName
                    }
                })
                .FirstOrDefaultAsync();

            if (bus == null)
            {
                return NotFound();
            }

            return Ok(bus);
        }

        // Crear un nuevo bus
        [HttpPost]
        public async Task<IActionResult> PostAsync(Bus bus)
        {
            _dataContext.Buses.Add(bus);
            await _dataContext.SaveChangesAsync();
            return Ok(bus);
        }

        // Actualizar un bus existente
        [HttpPut]
        public async Task<IActionResult> PutAsync(Bus bus)
        {
            _dataContext.Buses.Update(bus);
            await _dataContext.SaveChangesAsync();
            return Ok(bus);
        }

        // Eliminar un bus por ID
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

        // Cambiar la compañía asociada a un autobús
        [HttpPut("{busId:int}/change-company/{companyId:int}")]
        public async Task<IActionResult> ChangeCompanyAsync(int busId, int companyId)
        {
            // Obtiene el bus por ID
            var bus = await _dataContext.Buses.FirstOrDefaultAsync(b => b.Id == busId);
            if (bus == null)
            {
                return NotFound($"Bus with ID {busId} not found.");
            }

            // Verifica si la compañía existe
            var company = await _dataContext.Companies.FirstOrDefaultAsync(c => c.Id == companyId);
            if (company == null)
            {
                return NotFound($"Company with ID {companyId} not found.");
            }

            // Actualiza el CompanyId del autobús
            bus.CompanyId = companyId;

            // Guarda los cambios
            await _dataContext.SaveChangesAsync();

            return Ok(new
            {
                Message = $"Bus with ID {busId} has been updated to Company ID {companyId}.",
                Bus = new
                {
                    bus.Id,
                    bus.Seats,
                    bus.Category,
                    bus.CompanyId
                }
            });
        }
    }
}
