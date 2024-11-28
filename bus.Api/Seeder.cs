using bus.Api.Helpers;
using bus.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace bus.Api
{
    public class Seeder
    {
        private readonly DataContext dataContext;
        private readonly IUserHelper userHelper;

        public Seeder(DataContext dataContext, 
            IUserHelper userHelper)
        {
            this.dataContext = dataContext;
            this.userHelper = userHelper;
        }

        // Método principal para realizar la siembra de datos
        public async Task SeedAsync()
        {
            // Asegúrate de que la base de datos esté creada
            await dataContext.Database.EnsureCreatedAsync();

            // Verifica y crea datos iniciales
            await CheckCompaniesAsync();
            await CheckDriversAsync();
            await CheckBusesAsync();
            await CheckDestinationsAsync();
            await CheckOriginsAsync();
            await CheckPassengersAsync();
            await CheckTripsAsync();
            await CheckTripDetailsAsync();
            await CheckRolesAsync();
            await CheckUserAsync("Eduardo", "Fong", "eduardo@gmail.com", "2232342987", "eduardo.jpeg", UserType.Admin);
            await CheckUserAsync("Alfredo", "Barranco", "alfredo@gmail.com", "7725619071", "alfredo.jpeg", UserType.Admin);
            await CheckUserAsync("Sebastian", "de los Santos", "sebastian@gmail.com", "2379875091", "sebastian.jpeg", UserType.Admin);
            await CheckUserAsync("Miguel", "Carrera", "miguel@gmail.com", "6534910923", "miguel.jpeg", UserType.Admin);
            await CheckUserAsync("Victor", "Montaño", "victor@gmail.com", "9892316830", "victor.jpeg", UserType.Admin);
            await CheckUserAsync("Erick", "Guevara", "erick@gmail.com", "7971188978", "erick.jpeg", UserType.Admin);

        }

        private async Task<User> CheckUserAsync(string firstName, string lastName, string email, string phoneNumber, string photo, UserType userType)
        {
            // Verificar si el usuario ya existe
            var user = await userHelper.GetUserAsync(email);
            if (user == null)
            {
                // Buscar un Trip por defecto para asociarlo al usuario si es necesario
                var trip = await dataContext.Trips.FirstOrDefaultAsync();
                if (trip == null)
                {
                    throw new Exception("No trips found to associate with the user.");
                }

                // Crear un nuevo usuario
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email.Split('@')[0],
                    PhoneNumber = phoneNumber,
                    Photo = photo,
                    UserType = userType
                };

                // Crear el usuario en la base de datos con una contraseña predeterminada
                var result = await userHelper.AddUserAsync(user, "contraseña");
                if (!result.Succeeded)
                {
                    throw new Exception($"Unable to create user {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

                // Asignar el rol al usuario
                await userHelper.AddUserToRoleAsync(user, userType.ToString());

                //await userHelper.AddUserToRoleAsync(user, userType.ToString());
            }
            return user;
        }

        private async Task CheckRolesAsync()
        {
            await userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await userHelper.CheckRoleAsync(UserType.Coordinator.ToString());
            await userHelper.CheckRoleAsync(UserType.Driver.ToString());
            await userHelper.CheckRoleAsync(UserType.Passenger.ToString());


        }

        private async Task CheckCompaniesAsync()
        {
            if (!dataContext.Companies.Any())
            {
                dataContext.Companies.Add(new Company { CompanyName = "ADO" });
                dataContext.Companies.Add(new Company { CompanyName = "AU" });
                await dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckDriversAsync()
        {
            if (!dataContext.Drivers.Any())
            {
                dataContext.Drivers.Add(new Driver { Name = "Juan Perez", License = "ABC123" });
                dataContext.Drivers.Add(new Driver { Name = "Rodrigo Lopez", License = "XYZ789" });
                await dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckBusesAsync()
        {
            var company = await dataContext.Companies.FirstOrDefaultAsync();
            if (company == null) return;

            if (!dataContext.Buses.Any())
            {
                dataContext.Buses.Add(new Bus { Seats = 40, Category = "Clase Premium", CompanyId = company.Id });
                dataContext.Buses.Add(new Bus { Seats = 30, Category = "Ejecutivo", CompanyId = company.Id });
                await dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckDestinationsAsync()
        {
            if (!dataContext.Destinations.Any())
            {
                dataContext.Destinations.Add(new Destination { Station = "Paseo Destino" });
                dataContext.Destinations.Add(new Destination { Station = "CAPU" });
                await dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckOriginsAsync()
        {
            if (!dataContext.Origins.Any())
            {
                dataContext.Origins.Add(new Origin { Station = "Santa Fe" });
                dataContext.Origins.Add(new Origin { Station = "Central del Norte" });
                await dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckPassengersAsync()
        {
            if (!dataContext.Passengers.Any())
            {
                dataContext.Passengers.Add(new Passenger { Name = "Carlos Ramirez", Seat = 1 });
                dataContext.Passengers.Add(new Passenger { Name = "Ana Martinez", Seat = 2 });
                await dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckTripsAsync()
        {
            var driver = await dataContext.Drivers.FirstOrDefaultAsync();
            var bus = await dataContext.Buses.FirstOrDefaultAsync();

            if (driver == null || bus == null) return;

            if (!dataContext.Trips.Any())
            {
                dataContext.Trips.Add(new Trip
                {
                    Price = 100.0f,
                    Schedule = new TimeSpan(10, 0, 0),
                    DepartureDate = DateTime.Now.AddDays(1),
                    ArrivalDate = DateTime.Now.AddDays(1).AddHours(2),
                    Platform = "A1",
                    DriverId = driver.Id,
                    BusId = bus.Id
                });

                dataContext.Trips.Add(new Trip
                {
                    Price = 150.0f,
                    Schedule = new TimeSpan(15, 0, 0),
                    DepartureDate = DateTime.Now.AddDays(2),
                    ArrivalDate = DateTime.Now.AddDays(2).AddHours(3),
                    Platform = "B1",
                    DriverId = driver.Id,
                    BusId = bus.Id
                });

                await dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckTripDetailsAsync()
        {
            var trip = await dataContext.Trips.FirstOrDefaultAsync();
            var passenger = await dataContext.Passengers.FirstOrDefaultAsync();
            var origin = await dataContext.Origins.FirstOrDefaultAsync();
            var destination = await dataContext.Destinations.FirstOrDefaultAsync();

            if (trip == null || passenger == null || origin == null || destination == null) return;

            if (!dataContext.TripDetails.Any())
            {
                dataContext.TripDetails.Add(new TripDetail
                {
                    AssignedSeat = 1,
                    PassengerId = passenger.Id,
                    TripId = trip.Id,
                    OriginId = origin.Id,
                    DestinationId = destination.Id
                });

                dataContext.TripDetails.Add(new TripDetail
                {
                    AssignedSeat = 2,
                    PassengerId = passenger.Id,
                    TripId = trip.Id,
                    OriginId = origin.Id,
                    DestinationId = destination.Id
                });

                await dataContext.SaveChangesAsync();
            }
        }
    }
}
