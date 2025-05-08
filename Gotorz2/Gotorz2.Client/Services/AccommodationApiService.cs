using Gotorz2.Client.Models;
using System.Net.Http.Json;

namespace Gotorz2.Client.Services
{
    public class AccommodationApiService
    {
        private readonly HttpClient _httpClient;

        public AccommodationApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Ændret metode navn og parametre så de passer til hoteller
        public async Task<List<Accommodation>> SearchHotelsAsync(string city, string checkInDate, string checkOutDate)
        {
            try
            {
                var url = $"api/hotels/search?city={Uri.EscapeDataString(city)}&checkInDate={Uri.EscapeDataString(checkInDate)}&checkOutDate={Uri.EscapeDataString(checkOutDate)}";
                Console.WriteLine($"Fetching hotels from URL: {url}");

                var hotels = await _httpClient.GetFromJsonAsync<List<Accommodation>>(url);
                return hotels ?? new List<Accommodation>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving hotels: {ex.Message}");
                return new List<Accommodation>();
            }
        }
    }
}
