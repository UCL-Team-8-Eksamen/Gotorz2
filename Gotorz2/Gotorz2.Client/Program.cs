using Gotorz2.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Brug HttpClientFactory korrekt!
builder.Services.AddHttpClient<TravelApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7023/");
});

// TILF�J DETTE for HotelApiService
builder.Services.AddHttpClient<HotelApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7023/");
});

//service som holder styr p� valgt fly og hotel
builder.Services.AddSingleton<TravelPackageState>();
//Service til at gemme rejsepakker (ikke lavet f�rdigt)
builder.Services.AddSingleton<TravelPackageService>();


await builder.Build().RunAsync();
