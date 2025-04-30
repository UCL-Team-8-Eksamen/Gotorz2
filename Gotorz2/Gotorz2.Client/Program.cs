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
    client.BaseAddress = new Uri("https://localhost:7023/"); // Brug samme base URL som TravelApiService
});

await builder.Build().RunAsync();
