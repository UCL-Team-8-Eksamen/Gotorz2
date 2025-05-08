using Gotorz2.Client.Models;

//service som holder styr på vores valgte fly og hotel data
namespace Gotorz2.Client.Services
{

    public class TravelPackageState
    {
        public RoundTripFlight? SelectedFlight { get; set; }
        public Accommodation? SelectedHotel { get; set; }

        public string? FlightOrigin { get; set; }
        public string? FlightDestination { get; set; }
        public DateTime? FlightDepartureDate { get; set; }
        public DateTime? FlightReturnDate { get; set; }
        public List<RoundTripFlight>? FlightSearchResults { get; set; }


        public string? AccommodationSearchCity { get; set; }
        public DateTime? HotelCheckIn { get; set; }
        public DateTime? HotelCheckOut { get; set; }
        public List<Accommodation>? AccommodationSearchResults { get; set; }


        public List<TravelPackage> PublishedPackages { get; set; } = new();
    }

}



