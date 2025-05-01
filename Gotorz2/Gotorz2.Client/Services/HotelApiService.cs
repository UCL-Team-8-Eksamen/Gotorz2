using Gotorz2.Client.Models;
using System.Net.Http.Json;

namespace Gotorz2.Client.Services
{
    public class HotelApiService
    {
        private readonly HttpClient _httpClient;

        public HotelApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Ændret metode navn og parametre så de passer til hoteller
        public async Task<List<AccommodationData>> SearchHotelsAsync(string city, string checkInDate, string checkOutDate)
        {
            try
            {
                var url = $"https://localhost:7023/api/hotels/search?city={Uri.EscapeDataString(city)}&checkInDate={Uri.EscapeDataString(checkInDate)}&checkOutDate={Uri.EscapeDataString(checkOutDate)}";
                Console.WriteLine($"Fetching hotels from URL: {url}");

                var hotels = await _httpClient.GetFromJsonAsync<List<AccommodationData>>(url);
                return hotels ?? new List<AccommodationData>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving hotels: {ex.Message}");
                return new List<AccommodationData>();
            }
        }
    }
}
