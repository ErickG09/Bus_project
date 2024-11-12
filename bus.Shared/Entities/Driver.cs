using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus.Shared.Entities
{
    public class Driver
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The field {0} must have a maximum of {1} characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} must have a maximum of {1} characters")]
        [Display(Name = "License")]
        public string License { get; set; }
    }

}
