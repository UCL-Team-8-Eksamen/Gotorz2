using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Indeholder IdentityDbContext, som er en EF Core DbContext med Identity-funktionalitet
using Microsoft.EntityFrameworkCore; // Indeholder grundlæggende EF Core funktioner, fx DbContext og DbSet

//Denne klasse forbinder Entity Framework Core med ASP.NET Identity og min database.
//Den fortæller, at vi bruger ApplicationUser som brugerobjekt og klargør databasen til at håndtere login, roller m.m.
namespace Gotorz.Data
{
    // ApplicationDbContext er vores databasekontekst, som styrer forbindelsen til databasen og tabellerne i den
    // Den arver fra IdentityDbContext med ApplicationUser som brugerklasse, så den inkluderer Identity-tabeller
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser>(options) // Her siger vi, at Identity skal bruge vores ApplicationUser-klasse
    {
        // Lige nu er klassen tom, men du kan tilføje DbSet<> her for andre tabeller, du vil have i databasen
        // Fx: public DbSet<Product> Products { get; set; }
    }
}
