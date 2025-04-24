using System.Net.Http.Json;

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
        public async Task<List<RoundTripFlight>> GetFlightInfoAsync(string origin, string destination, string date, string returnDate)
        {
            try
            {
                var flights = await _httpClient.GetFromJsonAsync<List<RoundTripFlight>>(
                    $"api/flights/search?origin={origin}&destination={destination}&date={date}&returnDate={returnDate}");
                return flights ?? new List<RoundTripFlight>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving flights: {ex.Message}");
                return new List<RoundTripFlight>();
            }
        }


    }
}