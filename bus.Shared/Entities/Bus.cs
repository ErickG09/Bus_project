using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus.Shared.Entities
{
    public class Bus
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Seats")]
        public int Seats { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} must have a maximum of {1} characters")]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The field {0} must have a maximum of {1} characters")]
        [Display(Name = "Company")]
        public string Company { get; set; }
    }

}
