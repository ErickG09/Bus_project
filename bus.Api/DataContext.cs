using bus.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace bus.Api
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // Definición de DbSets para cada entidad
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripDetail> TripDetails { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Origin> Origins { get; set; }

        // Configuración de la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ejemplo de configuración de índices únicos (ajústalo según tus necesidades)
            modelBuilder.Entity<Company>().HasIndex(x => x.CompanyName).IsUnique();
            modelBuilder.Entity<Driver>().HasIndex(x => x.License).IsUnique();
            modelBuilder.Entity<Destination>().HasIndex(x => x.Station).IsUnique();
            modelBuilder.Entity<Origin>().HasIndex(x => x.Station).IsUnique();
            modelBuilder.Entity<Bus>().HasIndex(x => new { x.Company, x.Category }).IsUnique();

            // Configura las relaciones y restricciones adicionales aquí si es necesario
        }
    }
}
