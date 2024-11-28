using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus.Shared.Entities
{
    public class Trip
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Price")]
        public float Price { get; set; }

        [Required]
        [Display(Name = "Schedule")]
        public TimeSpan Schedule { get; set; }

        [Required]
        [Display(Name = "Departure Date")]
        public DateTime DepartureDate { get; set; }

        [Display(Name = "Arrival Date")]
        public DateTime? ArrivalDate { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} must have a maximum of {1} characters")]
        [Display(Name = "Platform")]
        public string Platform { get; set; } = null!;

        // Relación con Driver (1 a *)
        [Required]
        public int DriverId { get; set; }
        public Driver Driver { get; set; } = null!;

        // Relación con Bus (* a 1)
        [Required]
        public int BusId { get; set; }
        public Bus Bus { get; set; } = null!;

        // Relación con TripDetail (1 a *)
        public ICollection<TripDetail> TripDetails { get; set; } = new List<TripDetail>();

    }

}
