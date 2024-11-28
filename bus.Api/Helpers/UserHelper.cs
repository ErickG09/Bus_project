using bus.Shared.DTOs;
using bus.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bus.Api.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly DataContext dataContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<User> signInManager;

        public UserHelper(DataContext dataContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            this.dataContext = dataContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                throw new Exception($"Role {roleName} does not exist.");
            }

            var result = await userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                throw new Exception($"Error assigning role {roleName} to user {user.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var result = await roleManager.CreateAsync(new IdentityRole { Name = roleName });
                if (!result.Succeeded)
                {
                    throw new Exception($"Error creating role {roleName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        public async Task<User?> GetUserAsync(string email)
        {
            return await dataContext.Users
                .Include(u => u.TripDetails)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginDTO login)
        {
            return await signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
