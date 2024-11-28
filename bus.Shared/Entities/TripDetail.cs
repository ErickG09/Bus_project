using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus.Shared.Entities
{
    public class TripDetail
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Assigned Seat")]
        public int AssignedSeat { get; set; }

        // Relación con Passenger (1 a 1)
        [Required]
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; } = null!;

        // Relación con Trip (* a 1)
        [Required]
        public int TripId { get; set; }
        public Trip Trip { get; set; } = null!;

        // Relación con Origin (* a 1)
        [Required]
        public int OriginId { get; set; }
        public Origin Origin { get; set; } = null!;

        // Relación con Destination (* a 1)
        [Required]
        public int DestinationId { get; set; }
        public Destination Destination { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!; // Clave foránea de Identity User
        public User User { get; set; } = null!; // Propiedad de navegación hacia User
    }

}
