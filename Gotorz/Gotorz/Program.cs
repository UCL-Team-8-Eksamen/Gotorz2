using Gotorz.Client.Services;
using Gotorz.Components;
using Gotorz.Components.Account;
using Gotorz.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;


var builder = WebApplication.CreateBuilder(args);

// Tilføjer Rate Limiting services og regler
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("FixedWindowPolicy", context =>
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: ip,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,                                // Max 10 requests
                Window = TimeSpan.FromMinutes(1),                // per minut
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 2                                   // Op til 2 i kø
            });
    });
});




// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient<TravelApiService>();

//Register TravelApiService
builder.Services.AddScoped<TravelApiService>();
//Register AccommodationApiService
builder.Services.AddScoped<AccommodationApiService>();
//Service som holder styr på valgt fly og hotel
builder.Services.AddSingleton<TravelPackageState>();
//Service til at gemme rejsepakker (ikke lavet færdigt)
builder.Services.AddSingleton<TravelPackageService>();

// Gør det muligt at dele AuthenticationState (loginstatus) ned gennem komponenttræet i Blazor.
// Dette gør det nemt for komponenter at tilgå brugerens logininformation via CascadingAuthenticationState.
builder.Services.AddCascadingAuthenticationState();

// Registrerer en scoped service, der håndterer adgang til brugeridentitetsoplysninger i appen.
// Scoped betyder, at en ny instans oprettes per HTTP-anmodning eller bruger-session.
builder.Services.AddScoped<IdentityUserAccessor>();

// Registrerer en scoped service, der håndterer navigations- og omdirigeringslogik relateret til login og autorisation.
builder.Services.AddScoped<IdentityRedirectManager>();

// Registrerer en AuthenticationStateProvider, som sørger for at bevare og regelmæssigt validere brugerens autentificeringstilstand.
// Scoped betyder her, at samme instans genbruges under en bruger-session.
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

// Konfigurerer autentificeringssystemet i appen.
// Sætter default skemaer for identitets- og eksterne login-cookies (fx for cookies og OAuth).
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;      // Brug det primære cookieskema til login
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;   // Brug ekstern login-cookie til social login osv.
})
// Tilføjer standard Identity cookie-baserede loginmekanismer.
.AddIdentityCookies();

// Henter forbindelsesstreng til databasen fra konfiguration (fx appsettings.json).
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Registrerer databasekontekst (EF Core) med SQL Server og den hentede forbindelsesstreng.
// Dette bruges til at gemme brugere, roller, og andre Identity-data.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Tilføjer en fejlhåndteringsside, der er nyttig under udvikling ved database-relaterede fejl.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Konfigurerer Identity Core, som er bruger- og rolleadministrationen i ASP.NET Core.
// Sætter bl.a. krav om bekræftet konto før login, og tilføjer roller, EF Core stores, og token generators.
builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()                            // Tilføjer rolleunderstøttelse
    .AddEntityFrameworkStores<ApplicationDbContext>()   // Bruger EF Core til at gemme brugere og roller
    .AddSignInManager()                                  // Tilføjer sign-in management (login, logout)
    .AddDefaultTokenProviders();                         // Tilføjer token generators (fx til reset af password)

// Registrerer en e-mail sender service som singleton.
// Her er det en no-op implementering, der ikke sender mails, men kan udskiftes med rigtige mailudbydere.
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// Bygger applikationen med de registrerede services.
var app = builder.Build();

// Opretter et scope til service-udtræk og kalder en metode til at sørge for, at roller og brugere seedes ind i databasen.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleSeeder.SeedRolesAndUsers(services);  // Seed initiale roller og testbrugere ved opstart
}

// Opretter endnu et scope til service-udtræk for at sikre, at "Admin"-rollen findes.
// Hvis ikke, oprettes den i Identity-rollen.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
}


// Konfiguration af HTTP pipeline afhængig af miljø.

// Hvis vi er i udviklingsmiljø, aktiveres debugging for WebAssembly og migrations-fejlsider.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    // I produktion bruges fejlhåndtering med en fejlside, og HTTP Strict Transport Security (HSTS) aktiveres.
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Tvinger brug af HTTPS ved omdirigering af HTTP-anmodninger.
app.UseHttpsRedirection();

app.UseRateLimiter(); // ← Dette aktiverer rate limiting i app’en

// Aktiverer statiske filer som CSS, JS, billeder osv.
app.UseStaticFiles();

// Aktiverer CSRF-beskyttelse (antiforgery tokens) for at forhindre angreb.
app.UseAntiforgery();

// Mapper Blazor Razor komponenter og opsætter forskellige rendering modes for server- og WebAssembly-klienter.
// Muliggør interaktivitet i både server- og client-side Blazor.
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Gotorz.Client._Imports).Assembly);

// Mapper ekstra endpoints, som Identity /Account Razor komponenterne kræver for login, register osv.
app.MapAdditionalIdentityEndpoints();

// Starter webapplikationen og begynder at lytte efter HTTP-anmodninger.
app.Run();

