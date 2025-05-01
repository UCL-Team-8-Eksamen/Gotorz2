using Gotorz2.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Brug HttpClientFactory korrekt!
builder.Services.AddHttpClient<TravelApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7023/");
});

// TILFØJ DETTE for HotelApiService
builder.Services.AddHttpClient<HotelApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7023/");
});

//service som holder styr på valgt fly og hotel
builder.Services.AddSingleton<TravelPackageState>();
//Service til at gemme rejsepakker (ikke lavet færdigt)
builder.Services.AddSingleton<TravelPackageService>();


await builder.Build().RunAsync();
