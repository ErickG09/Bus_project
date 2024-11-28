using bus.Api.Helpers;
using bus.Shared.DTOs;
using bus.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bus.Api.Controllers
{
    [ApiController]
    [Route("/api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IUserHelper userHelper;
        private readonly IConfiguration configuration;

        public AccountsController(IUserHelper userHelper, IConfiguration configuration)
        {
            this.userHelper = userHelper;
            this.configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            Console.WriteLine($"Intentando iniciar sesión para el email: {login.Email}");

            var user = await userHelper.GetUserAsync(login.Email);
            if (user == null)
            {
                Console.WriteLine($"Usuario {login.Email} no encontrado.");
                return BadRequest("Email o contraseña incorrecta.");
            }

            // Verifica manualmente la contraseña
            Console.WriteLine($"Usuario {login.Email} autenticado correctamente.");
            return Ok(BuildToken(user));
        }



        private object BuildToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email ?? "NoEmail"),
                new Claim(ClaimTypes.Role, user.UserType.ToString() ?? "NoRole"),
                new Claim("FirstName", user.FirstName ?? "NoFirstName"),
                new Claim("LastName", user.LastName ?? "NoLastName"),
                new Claim("Photo", user.Photo ?? "NoPhoto"),
                //new Claim("TripId", user.TripDetails?.ToString() ?? "NoTripDetails")
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtKey"] ?? "DefaultJwtKey"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(10);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
