using bus.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bus.Api
{
    public class DataContext : IdentityDbContext<User>
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

            // Configuración de índices únicos
            modelBuilder.Entity<Company>().HasIndex(x => x.CompanyName).IsUnique();
            modelBuilder.Entity<Driver>().HasIndex(x => x.License).IsUnique();
            modelBuilder.Entity<Destination>().HasIndex(x => x.Station).IsUnique();
            modelBuilder.Entity<Origin>().HasIndex(x => x.Station).IsUnique();

            // Relación Company - Bus 
            modelBuilder.Entity<Bus>()
                .HasOne(b => b.Company)
                .WithMany(c => c.Buses)
                .HasForeignKey(b => b.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Driver - Trip 
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Driver)
                .WithMany(d => d.Trips)
                .HasForeignKey(t => t.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Bus - Trip 
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Bus)
                .WithMany()
                .HasForeignKey(t => t.BusId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Trip - TripDetail 
            modelBuilder.Entity<Trip>()
                .HasMany(t => t.TripDetails)
                .WithOne(td => td.Trip)
                .HasForeignKey(td => td.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación TripDetail - Passenger 
            modelBuilder.Entity<TripDetail>()
                .HasOne(td => td.Passenger)
                .WithMany()
                .HasForeignKey(td => td.PassengerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación TripDetail - Origin 
            modelBuilder.Entity<TripDetail>()
                .HasOne(td => td.Origin)
                .WithMany()
                .HasForeignKey(td => td.OriginId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación TripDetail - Destination 
            modelBuilder.Entity<TripDetail>()
                .HasOne(td => td.Destination)
                .WithMany()
                .HasForeignKey(td => td.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
