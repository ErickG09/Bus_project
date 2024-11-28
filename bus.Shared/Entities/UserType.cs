using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus.Shared.Entities
{
    public enum UserType
    {
        Admin,       // Administrador del sistema, con acceso completo
        Coordinator, // Encargado de la logística y la gestión de viajes y recursos
        Driver,      // Conductor, responsable de realizar los viajes
        Passenger    // Usuario final, quien reserva y utiliza los servicios

    }
}
