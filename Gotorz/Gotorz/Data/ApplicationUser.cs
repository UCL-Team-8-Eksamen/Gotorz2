using Microsoft.AspNetCore.Identity;

namespace Gotorz.Data
{
    // ApplicationUser arver fra IdentityUser, som er den standard brugerklasse i ASP.NET Identity
    // Ved at arve fra IdentityUser f�r vi automatisk funktioner som brugernavn, email, password hash 
    // Her kan du tilf�je ekstra egenskaber/properties til brugeren, f.eks. navn, adresse osv.
    public class ApplicationUser : IdentityUser
    {
        // Lige nu er denne klasse tom, s� den bruger standardfunktionaliteten fra IdentityUser
        // Men den er klar til udvidelse, hvis vi vil tilf�je brugerdata senere
    }
}
