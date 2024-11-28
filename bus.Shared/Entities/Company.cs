using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus.Shared.Entities
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The field {0} must have a maximum of {1} characters")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; } = null!;

        [Display(Name = "Buses")]
        public ICollection<Bus> Buses { get; set; } = new List<Bus>(); // Relación uno a muchos, inicializada como colección vacía
    }

}
