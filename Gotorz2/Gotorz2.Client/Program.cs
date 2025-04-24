using Gotorz2.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configure HttpClient to communicate with my API
// HttpClient peger på vores API-baseadresse
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7023/") }); // Replace with your API URL

// Register TravelApiService
builder.Services.AddScoped<TravelApiService>();

await builder.Build().RunAsync();