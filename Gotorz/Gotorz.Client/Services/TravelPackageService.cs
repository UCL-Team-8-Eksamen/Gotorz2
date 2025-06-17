using Gotorz.Client.Models;
using System.Net.Http.Json;

//Opgave: At gemme en TravelPackage til backend-server via en HTTP POST-request.
// Denne klasse fungerer som en service til at kommunikere med backend-API'et angående rejsepakker
public class TravelPackageService
{
    // Privat readonly felt bruges til at sende HTTP-forespørgsler
    private readonly HttpClient _http;

    // Constructor der modtager en HttpClient 
    public TravelPackageService(HttpClient http)
    {
        // Initialiserer det private _http-felt med den HttpClient
        _http = http;
    }

    // Denne metode sender en ny rejsepakke til backend-API'et for at blive gemt i databasen
    public async Task SaveTravelPackageAsync(TravelPackage package)
    {
        // Sender en HTTP POST-forespørgsel til "api/travelpackage" med objektet `package` som JSON-body
        var response = await _http.PostAsJsonAsync("api/travelpackage", package);

        // Sørger for at kaste en exception hvis svaret fra serveren ikke har en successtatuskode (2xx)
        // Hvis serveren f.eks. svarer med 400 eller 500, udløses en fejl
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<TravelPackage>> GetAllTravelPackagesAsync()
    {
        return await _http.GetFromJsonAsync<List<TravelPackage>>("api/TravelPackages");
    }

}
