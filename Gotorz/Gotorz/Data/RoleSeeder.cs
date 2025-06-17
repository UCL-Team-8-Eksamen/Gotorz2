using Microsoft.AspNetCore.Identity;

namespace Gotorz.Data
{
    // Denne kode opretter rollerne "Admin", "User" og "Sales" i systemet,
    // hvis de ikke allerede findes. Den bruges til at sikre, at nødvendige brugerroller er tilgængelige ved opstart.
    public class RoleSeeder
    {
        // Denne metode sørger for at oprette roller og brugere asynkront ved hjælp af services fra dependency injection
        public static async Task SeedRolesAndUsers(IServiceProvider serviceProvider)
        {
            // Henter RoleManager servicen, som bruges til at administrere roller i Identity-systemet
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Henter UserManager servicen, som bruges til at administrere brugere i Identity-systemet
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Definerer de roller, som vi ønsker at sikre findes i systemet
            var roles = new[] { "Admin", "User", "Sales" };

            // Gennemløber hver rolle i arrayet
            foreach (var role in roles)
            {
                // Tjekker om rollen allerede findes i systemet
                if (!await roleManager.RoleExistsAsync(role))
                {
                    // Opretter rollen, hvis den ikke findes
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
