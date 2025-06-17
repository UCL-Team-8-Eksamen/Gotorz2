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

// TILF�J DETTE for AccommodationApiService
builder.Services.AddHttpClient<AccommodationApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7023/");
});

// TILF�J DETTE for TravelPackageService, fordi den bruger HttpClient i sin constructor
builder.Services.AddHttpClient<TravelPackageService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7023/");
});


//service som holder styr p� valgt fly og hotel
builder.Services.AddSingleton<TravelPackageState>();


//Denne kode konfigurerer autentificering og autorisation i en Blazor WebAssembly-app.
//Den aktiverer autorisationssystemet, s�rger for at autentificeringsstatus kan deles
//ned gennem komponenttr�et, og bruger en PersistentAuthenticationStateProvider til at huske loginstatus, fx via localStorage eller en anden metode.
//Resultatet er, at komponenter kan reagere korrekt p�, om brugeren er logget ind eller ej.

// Tilf�jer underst�ttelse for autorisation (f.eks. brug af [Authorize]-attributten) i Blazor WebAssembly.
// Dette g�r det muligt at styre adgang til komponenter baseret p� brugerens rettigheder eller loginstatus.
//F�rste linje aktiverer autorisation.
builder.Services.AddAuthorizationCore();

// G�r det muligt at dele AuthenticationState (loginstatus) ned gennem komponenttr�et.
// Det betyder, at komponenter kan bruge <CascadingAuthenticationState> og AuthenticationStateProvider til at l�se loginstatus.
//Anden linje g�r loginstatus tilg�ngelig for komponenter.
builder.Services.AddCascadingAuthenticationState();

// Registrerer en brugerdefineret AuthenticationStateProvider, der holder styr p� loginstatus.
// PersistentAuthenticationStateProvider kan fx gemme loginstatus i localStorage eller sessionStorage, s� login overlever genindl�sning af siden.
// Den bliver tilg�ngelig som singleton � dvs. �n instans bruges i hele appen.
//Tredje linje bruger en brugerdefineret login-h�ndtering, som typisk husker status i browseren.
//N�r nogen i appen har brug for AuthenticationStateProvider, s� skal du give dem en instans af PersistentAuthenticationStateProvider.
//PersistentAuthenticationStateProvider , Denne klasse styrer hvordan login-status (AuthenticationState) bliver genereret og gjort tilg�ngelig i din app.
//Her er de vigtigste ting, den g�r:
//Hvis brugeren ikke er logget ind. Den returnerer en "tom" ClaimsPrincipal, alts� en anonym bruger uden identitet.
//Hvis der findes brugerdata i PersistentComponentState.Den fors�ger at hente brugeroplysninger (fx efter login), og hvis de findes, s�:
//Opretter den Claims (navn, ID, email osv.).Bygger en autentificeret ClaimsPrincipal og returnerer det som den aktuelle AuthenticationState
//N�r Blazor vil vide om brugeren er logget ind eller ej.public override Task<AuthenticationState> GetAuthenticationStateAsync() => authenticationStateTask;
//Blazor kalder denne metode automatisk, n�r den skal finde ud af login-status.
//Hvordan det hele spiller sammen i praksis:
//builder.Services registrerer PersistentAuthenticationStateProvider som leverand�r af login-status
//N�r din app k�rer, og fx en side bruger <AuthorizeView> eller [Authorize], sp�rger Blazor AuthenticationStateProvider om brugerens status.
//Blazor kalder GetAuthenticationStateAsync() i din PersistentAuthenticationStateProvider.
//Den returnerer enten:En anonym bruger (hvis der ikke er login-info) eller en autentificeret bruger, bygget ud fra claims (hvis login-info blev gemt tidligere)

//builder.Services binder min klasse til Blazor-systemet.PersistentAuthenticationStateProvider beskriver pr�cist hvordan loginstatus bestemmes
//De to arbejder sammen for at f� login/autorisationssystemet til at virke korrekt i en Blazor WebAssembly-app

//dvs.systemet bruger Claims-baseret autorisation, hvor brugerens rolleinformation er gemt som claims
//loginstatus deles effektivt gennem komponenttr�et via CascadingAuthenticationState
//autorisationstjek sker b�de via [Authorize]-attributter og AuthorizeView-komponenter for fleksibel og sikker adgangsstyring.

//et autorisationssystem i Blazor som sikrer, at brugerne kun f�r adgang til de sider og funktioner, de har rettigheder til
//Systemet h�ndterer loginstatus og roller ved hj�lp af en brugerdefineret AuthenticationStateProvider, som husker hvem brugeren er, og hvilke roller de har, ogs� selv efter siden opdateres.
//Desuden bruger jeg Blazors indbyggede autorisationsmekanismer til at beskytte komponenter og sider med rollebaseret adgangskontrol, s� vi nemt kan definere, hvem der m� se hvad i applikationen
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();


await builder.Build().RunAsync();
