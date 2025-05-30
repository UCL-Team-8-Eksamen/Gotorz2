﻿using Gotorz.Client.Models;
using System.Net.Http.Json;

namespace Gotorz.Client.Services
{
    public class TravelApiService
    {
        private readonly HttpClient _httpClient;

        public TravelApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RoundTripFlight>> GetFlightInfoAsync(string origin, string destination, string depatureDate, string returnDate)
        {
            try
            {
                var url = $"api/flights/search?origin={Uri.EscapeDataString(origin)}&destination={Uri.EscapeDataString(destination)}&date={Uri.EscapeDataString(depatureDate)}&returnDate={Uri.EscapeDataString(returnDate)}";
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