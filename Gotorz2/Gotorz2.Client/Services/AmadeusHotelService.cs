using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Gotorz2.Client.Models;

namespace Gotorz2.Client.Services
{
    public class AmadeusHotelService
    {
        private readonly HttpClient _http;
        private string _accessToken;
        private DateTime _tokenExpires;

        private const string apiKey = "GeBSW2od9ZEAqtZjN9G1niuI7qM15Bf3";
        private const string apiSecret = "UW5AR1G2fN8ez8A1";

        public AmadeusHotelService(HttpClient http)
        {
            _http = http;
        }

        private async Task AuthenticateAsync()
        {
            if (!string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _tokenExpires)
                return;

            var formData = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", apiKey },
                { "client_secret", apiSecret }
            };

            var response = await _http.PostAsync("https://test.api.amadeus.com/v1/security/oauth2/token", new FormUrlEncodedContent(formData));
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            _accessToken = doc.RootElement.GetProperty("access_token").GetString();
            int expiresIn = doc.RootElement.GetProperty("expires_in").GetInt32();
            _tokenExpires = DateTime.UtcNow.AddSeconds(expiresIn - 60); // buffer
        }

        public async Task<string> SearchHotelOffersAsync(string cityCode = "CPH", string checkIn = "2025-06-01", string checkOut = "2025-06-05")
        {
            await AuthenticateAsync();

            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://test.api.amadeus.com/v2/shopping/hotel-offers?cityCode={cityCode}&adults=1&checkInDate={checkIn}&checkOutDate={checkOut}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await _http.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
