using Gotorz.Client;
using Gotorz.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Brug HttpClientFactory korrekt!
builder.Services.AddHttpClient<TravelApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7023/");
});

// TILFØJ DETTE for AccommodationApiService
builder.Services.AddHttpClient<AccommodationApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7023/");
});

// TILFØJ DETTE for TravelPackageService, fordi den bruger HttpClient i sin constructor
builder.Services.AddHttpClient<TravelPackageService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7023/");
});


//service som holder styr på valgt fly og hotel
builder.Services.AddSingleton<TravelPackageState>();


//Denne kode konfigurerer autentificering og autorisation i en Blazor WebAssembly-app.
//Den aktiverer autorisationssystemet, sørger for at autentificeringsstatus kan deles
//ned gennem komponenttræet, og bruger en PersistentAuthenticationStateProvider til at huske loginstatus, fx via localStorage eller en anden metode.
//Resultatet er, at komponenter kan reagere korrekt på, om brugeren er logget ind eller ej.

// Tilføjer understøttelse for autorisation (f.eks. brug af [Authorize]-attributten) i Blazor WebAssembly.
// Dette gør det muligt at styre adgang til komponenter baseret på brugerens rettigheder eller loginstatus.
//Første linje aktiverer autorisation.
builder.Services.AddAuthorizationCore();

// Gør det muligt at dele AuthenticationState (loginstatus) ned gennem komponenttræet.
// Det betyder, at komponenter kan bruge <CascadingAuthenticationState> og AuthenticationStateProvider til at læse loginstatus.
//Anden linje gør loginstatus tilgængelig for komponenter.
builder.Services.AddCascadingAuthenticationState();

// Registrerer en brugerdefineret AuthenticationStateProvider, der holder styr på loginstatus.
// PersistentAuthenticationStateProvider kan fx gemme loginstatus i localStorage eller sessionStorage, så login overlever genindlæsning af siden.
// Den bliver tilgængelig som singleton – dvs. én instans bruges i hele appen.
//Tredje linje bruger en brugerdefineret login-håndtering, som typisk husker status i browseren.
//Når nogen i appen har brug for AuthenticationStateProvider, så skal du give dem en instans af PersistentAuthenticationStateProvider.
//PersistentAuthenticationStateProvider , Denne klasse styrer hvordan login-status (AuthenticationState) bliver genereret og gjort tilgængelig i din app.
//Her er de vigtigste ting, den gør:
//Hvis brugeren ikke er logget ind. Den returnerer en "tom" ClaimsPrincipal, altså en anonym bruger uden identitet.
//Hvis der findes brugerdata i PersistentComponentState.Den forsøger at hente brugeroplysninger (fx efter login), og hvis de findes, så:
//Opretter den Claims (navn, ID, email osv.).Bygger en autentificeret ClaimsPrincipal og returnerer det som den aktuelle AuthenticationState
//Når Blazor vil vide om brugeren er logget ind eller ej.public override Task<AuthenticationState> GetAuthenticationStateAsync() => authenticationStateTask;
//Blazor kalder denne metode automatisk, når den skal finde ud af login-status.
//Hvordan det hele spiller sammen i praksis:
//builder.Services registrerer PersistentAuthenticationStateProvider som leverandør af login-status
//Når din app kører, og fx en side bruger <AuthorizeView> eller [Authorize], spørger Blazor AuthenticationStateProvider om brugerens status.
//Blazor kalder GetAuthenticationStateAsync() i din PersistentAuthenticationStateProvider.
//Den returnerer enten:En anonym bruger (hvis der ikke er login-info) eller en autentificeret bruger, bygget ud fra claims (hvis login-info blev gemt tidligere)

//builder.Services binder min klasse til Blazor-systemet.PersistentAuthenticationStateProvider beskriver præcist hvordan loginstatus bestemmes
//De to arbejder sammen for at få login/autorisationssystemet til at virke korrekt i en Blazor WebAssembly-app

//dvs.systemet bruger Claims-baseret autorisation, hvor brugerens rolleinformation er gemt som claims
//loginstatus deles effektivt gennem komponenttræet via CascadingAuthenticationState
//autorisationstjek sker både via [Authorize]-attributter og AuthorizeView-komponenter for fleksibel og sikker adgangsstyring.

//et autorisationssystem i Blazor som sikrer, at brugerne kun får adgang til de sider og funktioner, de har rettigheder til
//Systemet håndterer loginstatus og roller ved hjælp af en brugerdefineret AuthenticationStateProvider, som husker hvem brugeren er, og hvilke roller de har, også selv efter siden opdateres.
//Desuden bruger jeg Blazors indbyggede autorisationsmekanismer til at beskytte komponenter og sider med rollebaseret adgangskontrol, så vi nemt kan definere, hvem der må se hvad i applikationen
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();


await builder.Build().RunAsync();
