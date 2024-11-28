using bus.Shared.DTOs;
using bus.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace bus.Api.Helpers
{
    public interface IUserHelper
    {
        public Task<User?> GetUserAsync(string email);
        public Task<IdentityResult> AddUserAsync(User user, string password);

        public Task CheckRoleAsync(string roleName);
        public Task AddUserToRoleAsync(User user, string roleName);
        
        public Task <bool> IsUserInRoleAsync(User user, string roleName);

        public Task<SignInResult> LoginAsync(LoginDTO login);

        Task LogoutAsync();
    }
}
