using System.Net.Http.Json;
using Gotorz2.Client.Models;

namespace Gotorz2.Client.Services
{
    public class TravelApiService
    {

        private readonly HttpClient _httpClient;

        public TravelApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        // Metode til at hente data fra API'et
        public async Task<List<Travel>> GetTravelsAsync()
        {
            try
            {
                var travels = await _httpClient.GetFromJsonAsync<List<Travel>>("api/travels");
                return travels ?? new List<Travel>();
            }
            catch (Exception ex)
            {
                // Håndter eventuelle fejl, som kan opstå under API kaldet
                Console.WriteLine($"Error retrieving travels: {ex.Message}");
                return new List<Travel>();
            }
        }
    }
}