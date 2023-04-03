using Infrastructure.Identity;
using Infrastructure.Persistence.DataGenerators;
using Infrastructure.Persistence.Sql.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence.Sql.Seed
{
    public class DatabaseInitializer
    {
        private readonly ILogger<DatabaseInitializer> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseInitializer(
            ILogger<DatabaseInitializer> logger, 
            ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the database initialization");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            if (!_context.Users.Any())
            {
                await AddDefaultUser();
            }

            if (!_context.Countries.Any() && !_context.Provinces.Any())
            {
                await AddCountriesAndProvinces();
            }
        }

        private async Task AddDefaultUser()
        {
            var administratorRole = new IdentityRole("Administrator");

            if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await _roleManager.CreateAsync(administratorRole);
            }

            // Default users
            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (_userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await _userManager.CreateAsync(administrator, "Administrator1!");
                if (!string.IsNullOrWhiteSpace(administratorRole.Name))
                {
                    await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
                }
            }
        }

        private async Task AddCountriesAndProvinces()
        {
            DataGenerator.InitBogusData();

            await _context.Countries.AddRangeAsync(DataGenerator.Countries);

            await _context.SaveChangesAsync();
        }
    }
}
