using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Gotorz2.Client.Services;
using Gotorz2.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configure HttpClient to communicate with the API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7023/") }); // Replace with your API URL

// Register TravelApiService
builder.Services.AddScoped<TravelApiService>();

builder.Services.AddScoped<AccomodationApiService>();

builder.Services.AddScoped<AmadeusHotelService>();

await builder.Build().RunAsync();