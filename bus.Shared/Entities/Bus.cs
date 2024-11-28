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
        public string Category { get; set; } = null!;

        // Relación con Company
        [Required]
        public int CompanyId { get; set; } // Clave foránea hacia Company
        public Company Company { get; set; } = null!; // Propiedad de navegación
    }

}
