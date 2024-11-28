using bus.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bus.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/tripdetails")]
    public class TripDetailController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public TripDetailController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Obtener todos los detalles del viaje con sus relaciones
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var tripDetails = await _dataContext.TripDetails
                .Include(td => td.Passenger) // Incluye el pasajero relacionado
                .Include(td => td.Trip)     // Incluye el viaje relacionado
                .Include(td => td.Origin)   // Incluye el origen relacionado
                .Include(td => td.Destination) // Incluye el destino relacionado
                .Select(td => new
                {
                    td.Id,
                    td.AssignedSeat,
                    Passenger = new
                    {
                        td.Passenger.Id,
                        td.Passenger.Name,
                        td.Passenger.Seat
                    },
                    Trip = new
                    {
                        td.Trip.Id,
                        td.Trip.Price,
                        td.Trip.Schedule,
                        td.Trip.DepartureDate,
                        td.Trip.ArrivalDate,
                        td.Trip.Platform
                    },
                    Origin = new
                    {
                        td.Origin.Id,
                        td.Origin.Station
                    },
                    Destination = new
                    {
                        td.Destination.Id,
                        td.Destination.Station
                    }
                })
                .ToListAsync();

            return Ok(tripDetails);
        }

        // Obtener un detalle del viaje específico con sus relaciones
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var tripDetail = await _dataContext.TripDetails
                .Include(td => td.Passenger) // Incluye el pasajero relacionado
                .Include(td => td.Trip)     // Incluye el viaje relacionado
                .Include(td => td.Origin)   // Incluye el origen relacionado
                .Include(td => td.Destination) // Incluye el destino relacionado
                .Where(td => td.Id == id)
                .Select(td => new
                {
                    td.Id,
                    td.AssignedSeat,
                    Passenger = new
                    {
                        td.Passenger.Id,
                        td.Passenger.Name,
                        td.Passenger.Seat
                    },
                    Trip = new
                    {
                        td.Trip.Id,
                        td.Trip.Price,
                        td.Trip.Schedule,
                        td.Trip.DepartureDate,
                        td.Trip.ArrivalDate,
                        td.Trip.Platform
                    },
                    Origin = new
                    {
                        td.Origin.Id,
                        td.Origin.Station
                    },
                    Destination = new
                    {
                        td.Destination.Id,
                        td.Destination.Station
                    }
                })
                .FirstOrDefaultAsync();

            if (tripDetail == null)
            {
                return NotFound();
            }

            return Ok(tripDetail);
        }

        // Crear un nuevo detalle de viaje
        [HttpPost]
        public async Task<IActionResult> PostAsync(TripDetail tripDetail)
        {
            _dataContext.TripDetails.Add(tripDetail);
            await _dataContext.SaveChangesAsync();
            return Ok(tripDetail);
        }

        // Actualizar un detalle de viaje existente
        [HttpPut]
        public async Task<IActionResult> PutAsync(TripDetail tripDetail)
        {
            _dataContext.TripDetails.Update(tripDetail);
            await _dataContext.SaveChangesAsync();
            return Ok(tripDetail);
        }

        // Eliminar un detalle de viaje por ID
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
