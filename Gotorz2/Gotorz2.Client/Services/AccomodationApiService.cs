using System.Net.Http;
using System.Net.Http.Json;
using Gotorz2.Client.Models;

namespace Gotorz2.Client.Services
{
    public class AccomodationApiService
    {
        private readonly HttpClient _http;

        public AccomodationApiService(HttpClient http)
        {
            _http = http;
        }

        // Henter alle accomodation entries
        public async Task<List<Accomodation>> GetAccomodationsAsync()
        {
            return await _http.GetFromJsonAsync<List<Accomodation>>("api/accomodations");
        }

        // Henter en enkelt accomodation baseret på dens indeks
        public async Task<Accomodation> GetAccomodationByIndexAsync(int index)
        {
            return await _http.GetFromJsonAsync<Accomodation>($"api/accomodations/{index}");
        }

        // Opretter en ny accomodation
        public async Task<HttpResponseMessage> CreateAccomodationAsync(Accomodation accomodation)
        {
            return await _http.PostAsJsonAsync("api/accomodations", accomodation);
        }

        // Opdaterer en eksisterende accomodation (baseret på indeks)
        public async Task<HttpResponseMessage> UpdateAccomodationByIndexAsync(int index, Accomodation accomodation)
        {
            return await _http.PutAsJsonAsync($"api/accomodations/{index}", accomodation);
        }

        // Sletter en accomodation baseret på indeks
        public async Task<HttpResponseMessage> DeleteAccomodationByIndexAsync(int index)
        {
            return await _http.DeleteAsync($"api/accomodations/{index}");
        }
    }
}
