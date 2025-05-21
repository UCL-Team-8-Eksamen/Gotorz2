using Gotorz.Client;
using Gotorz.Client.Services;
using Microsoft.Extensions.Http;
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

//service som holder styr på valgt fly og hotel
builder.Services.AddSingleton<TravelPackageState>();
//Service til at gemme rejsepakker (ikke lavet færdigt)
builder.Services.AddSingleton<TravelPackageService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

await builder.Build().RunAsync();
