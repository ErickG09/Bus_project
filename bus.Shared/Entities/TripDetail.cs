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

        [Required]
        public Passenger Passenger { get; set; }

        [Required]
        public Trip Trip { get; set; }
    }

}
