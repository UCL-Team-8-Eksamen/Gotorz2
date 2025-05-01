using Gotorz2.Client.Models;
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

        public async Task<List<RoundTripFlight>> GetFlightInfoAsync(string origin, string destination, string date, string returnDate)
        {
            try
            {
                var url = $"https://localhost:7023/api/flights/search?origin={Uri.EscapeDataString(origin)}&destination={Uri.EscapeDataString(destination)}&date={Uri.EscapeDataString(date)}&returnDate={Uri.EscapeDataString(returnDate)}";
                Console.WriteLine($"Fetching flights from URL: {url}");

                var flights = await _httpClient.GetFromJsonAsync<List<RoundTripFlight>>(url);
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