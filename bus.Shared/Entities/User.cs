using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bus.Shared.Entities
{
    public class User : IdentityUser
    {
        // Nombre del usuario
        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} must have a maximum of {1} characters")]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; } = null!;

        // Apellido del usuario
        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} must have a maximum of {1} characters")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; } = null!;

        // Foto de perfil 
        [Display(Name = "Foto")]
        public string? Photo { get; set; }

        // Tipo de usuario (Admin, Manager, etc.)
        [Required]
        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }

        // Relación con otras entidades (si aplica)
        public ICollection<TripDetail>? TripDetails { get; set; } = new List<TripDetail>();

        // Propiedad de sólo lectura para mostrar nombre completo
        public string FullName => $"{FirstName} {LastName}";

        // Fecha de creación del usuario
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Fecha de última modificación
        public DateTime? UpdatedAt { get; set; }
    }
}
