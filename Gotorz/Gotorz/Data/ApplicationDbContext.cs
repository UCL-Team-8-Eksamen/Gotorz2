using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Indeholder IdentityDbContext, som er en EF Core DbContext med Identity-funktionalitet
using Microsoft.EntityFrameworkCore; // Indeholder grundl�ggende EF Core funktioner, fx DbContext og DbSet

//Denne klasse forbinder Entity Framework Core med ASP.NET Identity og min database.
//Den fort�ller, at vi bruger ApplicationUser som brugerobjekt og klarg�r databasen til at h�ndtere login, roller m.m.
namespace Gotorz.Data
{
    // ApplicationDbContext er vores databasekontekst, som styrer forbindelsen til databasen og tabellerne i den
    // Den arver fra IdentityDbContext med ApplicationUser som brugerklasse, s� den inkluderer Identity-tabeller
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser>(options) // Her siger vi, at Identity skal bruge vores ApplicationUser-klasse
    {
        // Lige nu er klassen tom, men du kan tilf�je DbSet<> her for andre tabeller, du vil have i databasen
        // Fx: public DbSet<Product> Products { get; set; }
    }
}
