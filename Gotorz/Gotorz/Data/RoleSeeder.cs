using Microsoft.AspNetCore.Identity;

namespace Gotorz.Data
{
	public class RoleSeeder
	{
		public static async Task SeedRolesAndUsers(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

			var roles = new[] { "Admin", "User", "Sales" };

			// Create roles if they don't exist
			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}
		}

	}
}
