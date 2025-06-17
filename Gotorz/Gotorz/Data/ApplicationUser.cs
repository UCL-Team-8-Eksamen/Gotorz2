using Microsoft.AspNetCore.Identity;

namespace Gotorz.Data
{
    // ApplicationUser arver fra IdentityUser, som er den standard brugerklasse i ASP.NET Identity
    // Ved at arve fra IdentityUser får vi automatisk funktioner som brugernavn, email, password hash 
    // Her kan du tilføje ekstra egenskaber/properties til brugeren, f.eks. navn, adresse osv.
    public class ApplicationUser : IdentityUser
    {
        // Lige nu er denne klasse tom, så den bruger standardfunktionaliteten fra IdentityUser
        // Men den er klar til udvidelse, hvis vi vil tilføje brugerdata senere
    }
}
