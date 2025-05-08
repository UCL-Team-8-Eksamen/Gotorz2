using Gotorz2.Client.Models;
using System.Net.Http.Json;

public class TravelPackageService
{
    private readonly HttpClient _http;

    public TravelPackageService(HttpClient http)
    {
        _http = http;
    }

    public async Task SaveTravelPackageAsync(TravelPackage package)
    {
        var response = await _http.PostAsJsonAsync("api/travelpackage", package);
        response.EnsureSuccessStatusCode();
    }
}
