using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Testtest.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configure HttpClient to communicate with the API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7023/") }); // Replace with your API URL

// Register TravelApiService
builder.Services.AddScoped<TravelApiService>();

await builder.Build().RunAsync();